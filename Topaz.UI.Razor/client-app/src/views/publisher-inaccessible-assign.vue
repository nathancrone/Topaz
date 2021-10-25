<template>
  <div>
    <DownloadContactsForm
      :contactIds="selectedContactIds"
    ></DownloadContactsForm>
    <div class="row mt-3">
      <ul class="nav nav-tabs flex-column flex-md-row">
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITHOUT_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITHOUT_VM)"
            href="#"
            >{{ availableViews.PHONE_WITHOUT_VM.name }}</a
          >
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITH_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITH_VM)"
            href="#"
            >{{ availableViews.PHONE_WITH_VM.name }}</a
          >
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.LETTER }"
            @click.prevent="setActiveView(availableViews.LETTER)"
            href="#"
            >{{ availableViews.LETTER.name }}</a
          >
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.COMPLETE }"
            @click.prevent="setActiveView(availableViews.COMPLETE)"
            href="#"
            >{{ availableViews.COMPLETE.name }}</a
          >
        </li>
      </ul>
    </div>
    <div class="row mt-3 no-gutters">
      <div
        v-if="availableAssignments.length !== 0"
        class="d-flex flex-wrap col"
      >
        <div class="flex-grow-1 mb-2">
          <a
            class="btn btn-sm btn-primary mr-1"
            href="#"
            @click.prevent="loadAssignments"
            >refresh</a
          >
          <a
            class="btn btn-sm btn-primary mr-1"
            :class="{
              disabled:
                assignmentUnassignedSelectedCount === assignmentUnassignedCount,
            }"
            href="#"
            @click.prevent="selectUnassigned"
            >select unassigned</a
          >
          <a
            class="btn btn-sm btn-primary mr-1"
            :class="{
              disabled:
                assignmentAssignedSelectedCount === assignmentAssignedCount,
            }"
            href="#"
            @click.prevent="selectAssigned"
            >select assigned</a
          >
          <a
            class="btn btn-sm btn-primary"
            :class="{ disabled: assignmentSelectedCount === 0 }"
            href="#"
            @click.prevent="toggleAll(false)"
            >deselect all</a
          >
        </div>
        <div class="flex-grow-1 flex-md-grow-0 mb-2">
          <div class="input-group mr-1">
            <template
              v-if="
                activeView !== availableViews.COMPLETE &&
                  assignmentUnassignedSelectedCount !== 0 &&
                  assignmentAssignedSelectedCount === 0
              "
            >
              <input
                type="text"
                class="form-control form-control-sm"
                v-model="assigneeSearch"
                autocomplete="off"
                placeholder="enter assignee"
                list="availableAssigneeList"
                :disabled="!isAssignmentSelected"
              />
              <datalist id="availableAssigneeList">
                <option v-for="(match, i) in assigneeMatches" :key="i">{{
                  match.name
                }}</option>
              </datalist>
              <div class="input-group-append">
                <a
                  class="btn btn-sm btn-primary mr-1"
                  :class="{
                    disabled: !isAssignmentSelected || !selectedAssignee,
                  }"
                  href="#"
                  @click.ctrl.prevent.exact="exportContacts"
                  @click.prevent.exact="assignContacts"
                  >assign</a
                >
              </div>
              <a
                v-if="assignmentOnlyUnavailableItemsSelected"
                class="btn btn-sm btn-success mr-1"
                href="#"
                @click.prevent="available"
                >flag available</a
              >
            </template>
            <template
              v-if="
                activeView !== availableViews.COMPLETE &&
                  assignmentUnassignedSelectedCount === 0 &&
                  assignmentAssignedSelectedCount !== 0
              "
            >
              <a
                v-if="activeView === availableViews.LETTER"
                class="btn btn-sm btn-primary mr-1"
                href="#"
                @click.prevent="lettersSent"
                >letters sent</a
              >
              <template v-else>
                <input
                  id="phoneResponseType"
                  type="text"
                  class="form-control form-control-sm"
                  v-model="responseTypeSearch"
                  autocomplete="off"
                  placeholder="enter result"
                  list="phoneResponseTypeList"
                />
                <datalist id="phoneResponseTypeList">
                  <option v-for="(type, r) in phoneResponseTypes" :key="r">{{
                    type.name
                  }}</option>
                </datalist>
                <div class="input-group-append">
                  <a
                    class="btn btn-sm btn-primary mr-1"
                    :class="{
                      disabled: !isAssignmentSelected || !selectedResponseType,
                    }"
                    href="#"
                    @click.prevent="saveResponses"
                    >save</a
                  >
                </div>
              </template>
              <a
                class="btn btn-sm btn-primary mr-1"
                href="#"
                @click.prevent="unassign"
                >unassign</a
              >
            </template>
            <a
              v-if="assignmentOnlyAvailableItemsSelected"
              class="btn btn-sm btn-outline-success mr-1"
              @click.prevent="unavailable"
              >unflag available</a
            >
            <a
              v-if="assignmentSelectedCount !== 0"
              class="btn btn-sm btn-primary"
              href="#"
              @click.prevent="download"
              >download</a
            >
          </div>
        </div>
      </div>
      <div v-else class="w-100 alert alert-primary" role="alert">
        no contacts in this category
      </div>
    </div>
    <div
      v-if="
        territoryExports.length !== 0 && activeView === availableViews.COMPLETE
      "
      class="row no-gutters"
    >
      <div class="col">
        <h3>Exports</h3>
        <table class="table table-sm">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Publisher</th>
              <th scope="col">Export Date</th>
              <th scope="col"># of Contacts</th>
              <th scope="col">&nbsp;</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="e in territoryExports"
              :key="`exportRow${e.inaccessibleTerritoryExportId}`"
            >
              <th scope="row">{{ e.inaccessibleTerritoryExportId }}</th>
              <td>{{ e.firstName }}&nbsp;{{ e.lastName }}</td>
              <td>{{ displayDate(e.exportDate) }}</td>
              <td>{{ e.exportItemCount }}</td>
              <td>
                <a
                  :href="
                    `/Inaccessible/GetTerritoryExportContacts/${e.inaccessibleTerritoryExportId}`
                  "
                  target="_blank"
                  >download</a
                >
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div
      v-if="availableAssignments.length !== 0"
      class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4"
    >
      <PublisherInaccessibleAssignCard
        v-for="a in availableAssignments"
        :key="`card${a.inaccessibleContactId}`"
        :contact="a"
        @change="handleAssignmentChange"
      />
    </div>
  </div>
</template>

<script>
import { data } from "../shared";
import { parseISO, format } from "date-fns";
import PublisherInaccessibleAssignCard from "../components/publisher-inaccessible-assign-card";

const VIEWS = Object.freeze({
  PHONE_WITHOUT_VM: {
    type: "phone",
    name: "1st Call (no voicemail)",
    activityTypeId: 1,
  },
  PHONE_WITH_VM: {
    type: "vm",
    name: "2nd Call (leave voicemail)",
    activityTypeId: 2,
  },
  LETTER: { type: "letter", name: "Write Letters", activityTypeId: 3 },
  COMPLETE: { type: "none", name: "Done", activityTypeId: null },
});

let DownloadContactsForm = {
  props: {
    contactIds: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {};
  },
  template: "#download-form-template",
  methods: {
    submit: function() {
      this.$refs.form.submit();
    },
  },
  created() {
    this.$parent.$on("downloadSubmit", this.submit);
  },
};

export default {
  name: "PublisherInaccessibleAssign",
  props: {
    id: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
      availableViews: VIEWS,
      activeView: VIEWS.PHONE_WITHOUT_VM,
      availableAssignments: [],
      assigneeSearch: "",
      assigneeSearchToken: undefined,
      availableAssignees: [],
      selectedAssignee: undefined,
      responseTypeSearch: "",
      phoneResponseTypes: [],
      selectedResponseType: undefined,
      territoryExports: [],
    };
  },
  components: {
    PublisherInaccessibleAssignCard,
    DownloadContactsForm,
  },
  async created() {
    await this.loadAssignments();
    await this.loadPhoneResponseTypes();
  },
  computed: {
    isAssignmentSelected() {
      return this.availableAssignments.some((a) => {
        return a.selected === true;
      });
    },
    assigneeMatches() {
      return this.availableAssignees.filter((a) => {
        return (
          a.name.toLowerCase().indexOf(this.assigneeSearch.toLowerCase()) >= 0
        );
      });
    },
    assignmentCount() {
      return this.availableAssignments.length;
    },
    assignmentSelectedCount() {
      return this.availableAssignments.filter((x) => x.selected).length;
    },
    assignmentUnassignedCount() {
      return this.availableAssignments.filter((x) => !x.assignPublisher).length;
    },
    assignmentUnassignedSelectedCount() {
      return this.availableAssignments.filter(
        (x) => !x.assignPublisher && x.selected
      ).length;
    },
    assignmentAssignedCount() {
      return this.availableAssignments.filter((x) => !!x.assignPublisher)
        .length;
    },
    assignmentAssignedSelectedCount() {
      return this.availableAssignments.filter(
        (x) => !!x.assignPublisher && x.selected
      ).length;
    },
    assignmentOnlyUnavailableItemsSelected() {
      return this.availableAssignments.some((x) => x.selected) && this.availableAssignments.filter((x) => x.selected).every((x) => !x.isAvailable && (!x.doNotContactPhone || !x.doNotContactLetter))
    },
    assignmentOnlyAvailableItemsSelected() {
      return this.availableAssignments.some((x) => x.selected) && this.availableAssignments.filter((x) => x.selected).every((x) => x.isAvailable)
    },
    selectedContactIds() {
      return this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
    },
  },
  methods: {
    async loadAssignments() {
      const assignments = await data.getAvailableInaccessibleAssignments(
        this.id,
        this.activeView.type
      );
      assignments.forEach(function(a) {
        a.selected = false;
      });
      this.availableAssignments = [...assignments];

      if (this.activeView === VIEWS.COMPLETE) {
        const territoryExports = await data.getTerritoryExports(this.id);
        this.territoryExports = [...territoryExports];
      }
    },
    async loadAssignees(token) {
      const assignees = await data.getPublisherSelectOptions(token);
      this.availableAssignees = [...assignees];
    },
    async loadPhoneResponseTypes() {
      const types = await data.getPhoneResponseTypes();
      this.phoneResponseTypes = [...types];
    },
    async setActiveView(v) {
      this.activeView = v;
      await this.loadAssignments();
    },
    async assignContacts() {
      const assignments = this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
      const assignee = this.selectedAssignee.id;
      await data.assignInaccessibleContacts(assignee, assignments);
      await this.loadAssignments();
      this.selectedAssignee = undefined;
      this.assigneeSearch = "";
      this.availableAssignees = [];
    },
    async exportContacts() {
      const assignments = this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
      const assignee = this.selectedAssignee.id;
      await data.saveInaccessibleContactExportActivities(assignee, assignments);
      await this.loadAssignments();
      this.selectedAssignee = undefined;
      this.assigneeSearch = "";
      this.availableAssignees = [];
    },
    async unassign() {
      const assignments = this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
      await data.unassignInaccessibleContacts(assignments);
      await this.loadAssignments();
    },

    async available() {
      const assignments = this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
      await data.flagAvailabilityInaccessibleContacts(assignments, true);
      await this.loadAssignments();
    },

    async unavailable() {
      const assignments = this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
      await data.flagAvailabilityInaccessibleContacts(assignments, false);
      await this.loadAssignments();
    },

    download() {
      this.$emit("downloadSubmit");
    },
    async saveResponses() {
      const assignments = this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
      const responseType = this.selectedResponseType.phoneResponseTypeId;
      const activityType = this.activeView.activityTypeId;
      await data.saveInaccessibleContactPhoneActivities(
        responseType,
        activityType,
        assignments
      );
      await this.loadAssignments();
      this.selectedAssignee = undefined;
      this.selectedResponseType = undefined;
      this.assigneeSearch = "";
      this.responseTypeSearch = "";
      this.availableAssignees = [];
    },
    async lettersSent() {
      const assignments = this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
      await data.saveInaccessibleContactLetterActivities(assignments);
      await this.loadAssignments();
      this.selectedAssignee = undefined;
      this.assigneeSearch = "";
      this.availableAssignees = [];
    },
    toggleAll(selected) {
      this.availableAssignments
        .filter((x) => x.selected !== selected)
        .forEach(function(a) {
          a.selected = selected;
        });
    },
    selectAssigned() {
      this.availableAssignments
        .filter((x) => x.assignPublisher && !x.selected)
        .forEach(function(a) {
          a.selected = true;
        });
    },
    selectUnassigned() {
      this.availableAssignments
        .filter(
          (x) => !x.doNotContactPhone && !x.assignPublisher && !x.selected
        )
        .forEach(function(a) {
          a.selected = true;
        });
    },
    handleAssignmentChange(assignment) {
      const index = this.availableAssignments.findIndex(
        (a) => a.inaccessibleContactId === assignment.inaccessibleContactId
      );
      this.availableAssignments.splice(index, 1, assignment);
      this.availableAssignments = [...this.availableAssignments];
    },
    displayDate(date) {
      return format(parseISO(date), "MMM dd, yyyy");
    },
  },
  watch: {
    async assigneeSearch(after) {
      // find available assignee that exactly matches
      const assignee = this.availableAssignees.find(
        ({ name }) => name.toLowerCase() === after.toLowerCase()
      );

      // if exact match found, set the selected assignee
      // if not, clear the selected assignee
      if (assignee) {
        this.selectedAssignee = Object.assign({}, assignee);
      } else {
        this.selectedAssignee = undefined;
      }

      if (after.length < 3 || this.selectedAssignee) {
        return;
      }
      if (
        this.assigneeSearchToken &&
        after.indexOf(this.assigneeSearchToken) !== -1
      ) {
        return;
      }
      this.assigneeSearchToken = after;
      await this.loadAssignees(after);
    },
    async responseTypeSearch(after) {
      // find available assignee that exactly matches
      const responseType = this.phoneResponseTypes.find(
        ({ name }) => name.toLowerCase() === after.toLowerCase()
      );

      // if exact match found, set the selected response type
      // if not, clear the selected response type
      if (responseType) {
        this.selectedResponseType = Object.assign({}, responseType);
      } else {
        this.selectedResponseType = undefined;
      }
    },
  },
};
</script>
