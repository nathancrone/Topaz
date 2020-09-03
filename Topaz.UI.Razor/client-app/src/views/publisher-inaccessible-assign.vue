<template>
  <div>
    <div class="row mt-3">
      <ul class="nav nav-tabs">
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITHOUT_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITHOUT_VM)"
            href="#"
          >{{ availableViews.PHONE_WITHOUT_VM.name }}</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.PHONE_WITH_VM }"
            @click.prevent="setActiveView(availableViews.PHONE_WITH_VM)"
            href="#"
          >{{ availableViews.PHONE_WITH_VM.name }}</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.LETTER }"
            @click.prevent="setActiveView(availableViews.LETTER)"
            href="#"
          >{{ availableViews.LETTER.name }}</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            :class="{ active: activeView === availableViews.COMPLETE }"
            @click.prevent="setActiveView(availableViews.COMPLETE)"
            href="#"
          >{{ availableViews.COMPLETE.name }}</a>
        </li>
      </ul>
    </div>
    <div class="row mt-3 no-gutters">
      <div class="d-flex flex-wrap col">
        <div class="flex-grow-1 mb-2">
          <a class="btn btn-sm btn-primary mr-1" href="#" @click.prevent="loadAssignments">refresh</a>
          <a
            class="btn btn-sm btn-primary mr-1"
            :class="{ disabled: assignmentCount === assignmentSelectedCount }"
            href="#"
            @click.prevent="toggleAll(true)"
          >select all</a>
          <a
            class="btn btn-sm btn-primary"
            :class="{ disabled: assignmentSelectedCount === 0 }"
            href="#"
            @click.prevent="toggleAll(false)"
          >deselect all</a>
        </div>
        <div class="flex-grow-1 flex-md-grow-0 mb-2">
          <div class="input-group mr-1">
            <template
              v-if="assignmentUnassignedSelectedCount !== 0 && assignmentAssignedSelectedCount === 0"
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
                <option v-for="(match, i) in assigneeMatches" :key="i">{{ match.name }}</option>
              </datalist>
              <div class="input-group-append">
                <a
                  class="btn btn-sm btn-primary mr-1"
                  :class="{ disabled: !isAssignmentSelected || !selectedAssignee }"
                  href="#"
                  @click.prevent="assign"
                >assign</a>
              </div>
            </template>
            <template
              v-if="assignmentUnassignedSelectedCount === 0 && assignmentAssignedSelectedCount !== 0"
            >
              <input
                id="phoneResponseType"
                type="text"
                class="form-control form-control-sm"
                autocomplete="off"
                placeholder="enter result"
                list="phoneResponseTypeList"
              />
              <datalist id="phoneResponseTypeList">
                <option v-for="(type, r) in phoneResponseTypes" :key="r">{{ type.name }}</option>
              </datalist>
              <div class="input-group-append">
                <a class="btn btn-sm btn-primary mr-1" href="#" @click.prevent="(0)">save</a>
              </div>
              <a class="btn btn-sm btn-primary mr-1" href="#" @click.prevent="unassign">unassign</a>
            </template>
          </div>
        </div>
      </div>
    </div>
    <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4">
      <template v-for="(a, i) in availableAssignments">
        <div :key="'c' + i" class="col mt-3">
          <div class="card shadow-sm rounded">
            <div class="card-header d-flex">
              <div class="flex-grow-1">{{ a.lastName }}, {{ a.firstName }} {{ a.middleInitial }}</div>
              <div class="form-check">
                <input type="checkbox" class="form-check-input" v-model="a.selected" :id="'cb' + i" />
              </div>
            </div>
            <div class="card-body">
              <span
                v-if="a.assignPublisher"
                class="badge badge-secondary"
              >{{ a.assignPublisher.firstName }} {{ a.assignPublisher.lastName }}</span>
              <address>
                Age: {{ a.age }}
                <br />
                {{ a.mailingAddress1 }}
                <br />
                {{ a.mailingAddress2 }}
                <br />
                Dallas, TX {{ a.postalCode }}
                <br />
                {{ a.phoneNumber }}
              </address>
              <ul class="list-group">
                <li class="list-group-item list-group-item-action flex-column align-items-start">
                  <div class="d-flex w-100 justify-content-end">
                    <i class="arrow up"></i>
                  </div>
                </li>
                <template>
                  <li class="list-group-item flex-column align-items-start">
                    <div class="mb-1 d-flex w-100 justify-content-between">
                      <small class="font-weight-bold">8/8/2020 - John Publisher</small>
                    </div>
                    <div class="d-flex w-100">
                      <small>Voicemail - No Name</small>
                    </div>
                    <div class="d-flex w-100">
                      <small class="font-italic">different language. possibly vietnamese</small>
                    </div>
                  </li>
                  <li class="list-group-item flex-column align-items-start">
                    <div class="mb-1 d-flex w-100 justify-content-between">
                      <small class="font-weight-bold">8/8/2020 - John Publisher</small>
                    </div>
                    <div class="d-flex w-100">
                      <small>Voicemail - No Name</small>
                    </div>
                    <div class="d-flex w-100">
                      <small class="font-italic">different language. possibly vietnamese</small>
                    </div>
                  </li>
                  <li class="list-group-item flex-column align-items-start">
                    <div class="mb-1 d-flex w-100 justify-content-between">
                      <small class="font-weight-bold">8/8/2020 - John Publisher</small>
                    </div>
                    <div class="d-flex w-100">
                      <small>Voicemail - No Name</small>
                    </div>
                    <div class="d-flex w-100">
                      <small class="font-italic">different language. possibly vietnamese</small>
                    </div>
                  </li>
                </template>
              </ul>
            </div>
          </div>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
import { data } from "../shared";

const VIEWS = Object.freeze({
  PHONE_WITHOUT_VM: { type: "phone", name: "1st Call (no voicemail)" },
  PHONE_WITH_VM: { type: "vm", name: "2nd Call (leave voicemail)" },
  LETTER: { type: "letter", name: "Write Letters" },
  COMPLETE: { type: "none", name: "Done" },
});

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
      phoneResponseTypes: [],
    };
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
    assignmentUnassignedSelectedCount() {
      return this.availableAssignments.filter(
        (x) => !x.assignPublisher && x.selected
      ).length;
    },
    assignmentAssignedSelectedCount() {
      return this.availableAssignments.filter(
        (x) => !!x.assignPublisher && x.selected
      ).length;
    },
  },
  methods: {
    async loadAssignments() {
      const assignments = await data.getAvailableInaccessibleAssignments(
        this.id,
        this.activeView.type
      );
      assignments.forEach(function (a) {
        a.selected = false;
      });
      this.availableAssignments = [];
      this.availableAssignments = assignments;
    },
    async loadAssignees(token) {
      const assignees = await data.getPublisherSelectOptions(token);
      this.availableAssignees = [];
      this.availableAssignees = assignees;
    },
    async loadPhoneResponseTypes() {
      const types = await data.getPhoneResponseTypes();
      this.phoneResponseTypes = types;
    },
    async setActiveView(v) {
      this.activeView = v;
      await this.loadAssignments();
    },
    async assign() {
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
    async unassign() {
      const assignments = this.availableAssignments.reduce(
        (a, o) => (o.selected && a.push(o.inaccessibleContactId), a),
        []
      );
      await data.unassignInaccessibleContacts(assignments);
      await this.loadAssignments();
    },
    toggleAll(selected) {
      this.availableAssignments.forEach(function (a) {
        a.selected = selected;
      });
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
  },
};
</script>
