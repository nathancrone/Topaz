(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-admin"],{"063a":function(t,e,r){},"0643":function(t,e,r){},1070:function(t,e,r){"use strict";var a=r("920b"),n=r.n(a);n.a},1245:function(t,e,r){"use strict";r.r(e);var a=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",[r("div",{staticClass:"d-flex"},[r("div",{staticClass:"flex-grow-1 mb-2"}),r("div",{staticClass:"flex-grow-1 flex-md-grow-0 mb-2"},[r("button",{staticClass:"close",attrs:{type:"button","aria-label":"Close"},on:{click:function(e){return t.$router.go(-1)}}},[r("span",{attrs:{"aria-hidden":"true"}},[t._v("×")])])])]),t._m(0)])},n=[function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",{staticClass:"row no-gutters"},[r("div",{staticClass:"col"},[r("h3",[t._v("Activity - E-015 / I-1006")]),r("table",{staticClass:"table table-sm"},[r("thead",[r("tr",[r("th",{attrs:{scope:"col"}},[t._v("Publisher")]),r("th",{attrs:{scope:"col"}},[t._v("Checked Out")]),r("th",{attrs:{scope:"col"}},[t._v("Checked In")]),r("th",{attrs:{scope:"col"}})])]),r("tbody",[r("tr",[r("td",[t._v("Nathan Crone")]),r("td",[t._v("Oct 08, 2020")]),r("td",[t._v("Nov 14, 2020")]),r("td",{staticClass:"text-right"},[r("a",{staticClass:"btn btn-sm btn-primary mr-1",attrs:{href:"#"}},[t._v("edit")]),r("a",{staticClass:"btn btn-sm btn-primary mr-1",attrs:{href:"#"}},[t._v("delete")])])]),r("tr",[r("td",[t._v("Patricia Crone")]),r("td",[t._v("Oct 08, 2019")]),r("td",[t._v("Nov 14, 2019")]),r("td",{staticClass:"text-right"},[r("a",{staticClass:"btn btn-sm btn-primary mr-1",attrs:{href:"#"}},[t._v("edit")]),r("a",{staticClass:"btn btn-sm btn-primary mr-1",attrs:{href:"#"}},[t._v("delete")])])]),r("tr",[r("td",[t._v("Dana Crone")]),r("td",[t._v("Oct 08, 2018")]),r("td",[t._v("Nov 14, 2018")]),r("td",{staticClass:"text-right"},[r("a",{staticClass:"btn btn-sm btn-primary mr-1",attrs:{href:"#"}},[t._v("edit")]),r("a",{staticClass:"btn btn-sm btn-primary mr-1",attrs:{href:"#"}},[t._v("delete")])])])])])])])}],s=(r("a9e3"),{name:"AdminTerritoryActivity",props:{id:{type:Number,default:0}},data:function(){return{}}}),i=s,o=r("2877"),c=Object(o["a"])(i,a,n,!1,null,null,null);e["default"]=c.exports},"3cae":function(t,e,r){"use strict";var a=r("0643"),n=r.n(a);n.a},"50ed":function(t,e,r){"use strict";var a=r("063a"),n=r.n(a);n.a},"920b":function(t,e,r){},"9ad2":function(t,e,r){"use strict";r.r(e);var a=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",[r("div",{staticClass:"row mt-3"},[r("ul",{staticClass:"nav nav-tabs flex-column flex-md-row"},[r("li",{staticClass:"nav-item"},[r("router-link",{class:{"nav-link":!0,active:"street"===t.type},attrs:{tag:"a",to:{name:"AdminTerritory",params:{type:"street"}}}},[t._v("Street")])],1),r("li",{staticClass:"nav-item"},[r("router-link",{class:{"nav-link":!0,active:"inaccessible"===t.type},attrs:{tag:"a",to:{name:"AdminTerritory",params:{type:"inaccessible"}}}},[t._v("Inaccessible")])],1)])]),t._m(0),r("div",[r("div",{staticClass:"row no-gutters"},[r("div",{staticClass:"col"},[r("h3",[t._v("Territories")]),r("table",{staticClass:"table table-sm"},[t._m(1),r("tbody",t._l(t.territories,(function(e){return r("tr",{key:e.territoryId,class:{"table-secondary":e.inActive}},[e.streetTerritoryCode?r("th",{attrs:{scope:"row"}},[t._v(" "+t._s(e.streetTerritoryCode)+" / "+t._s(e.territoryCode)+" ")]):r("th",{attrs:{scope:"row"}},[t._v(t._s(e.territoryCode))]),e.activity?[r("td",[t._v(" "+t._s(e.publisher.firstName)+" "+t._s(e.publisher.lastName)+" ")]),r("td",[t._v(t._s(t.displayDate(e.activity.checkOutDate)))]),e.activity.checkInDate?r("td",[t._v(" "+t._s(t.displayDate(e.activity.checkInDate))+" ")]):r("td",[t._v("-")])]:[r("td",{staticClass:"text-center",attrs:{colspan:"3"}},[t._v("-")])],r("td",{staticClass:"text-right"},[e.inActive?t._e():[e.activity&&!e.activity.checkInDate?r("a",{staticClass:"btn btn-sm btn-primary mr-1",attrs:{href:"#"},on:{click:function(r){return r.preventDefault(),t.checkIn(e)}}},[t._v("check in")]):r("a",{staticClass:"btn btn-sm btn-primary mr-1",attrs:{href:"#"},on:{click:function(r){return r.preventDefault(),t.checkOut(e)}}},[t._v("check out")])],r("router-link",{staticClass:"btn btn-sm btn-primary",attrs:{tag:"a",to:{name:"AdminTerritoryActivity",params:{id:e.territoryId}}}},[t._v("activity")])],2)],2)})),0)])])])]),r("AdminTerritoryCheckinModal",{attrs:{territory:t.selectedTerritory,open:t.isCheckinModalOpen},on:{close:function(e){t.isCheckinModalOpen=!1},confirm:t.checkinConfirm}}),r("AdminTerritoryCheckoutModal",{attrs:{territory:t.selectedTerritory,open:t.isCheckoutModalOpen},on:{close:function(e){t.isCheckoutModalOpen=!1},confirm:t.checkoutConfirm}})],1)},n=[function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",{staticClass:"row mt-3 no-gutters"},[r("div",{staticClass:"d-flex flex-wrap col"},[r("div",{staticClass:"flex-grow-1 mb-2"}),r("div",{staticClass:"flex-grow-1 flex-md-grow-0 mb-2"},[r("div",{staticClass:"btn-group btn-group-sm",attrs:{role:"group","aria-label":"filter"}},[r("button",{staticClass:"btn btn-secondary active",attrs:{type:"button"}},[t._v(" All ")]),r("button",{staticClass:"btn btn-secondary",attrs:{type:"button"}},[t._v(" Checked Out ")]),r("button",{staticClass:"btn btn-secondary",attrs:{type:"button"}},[t._v(" Checked In ")])])])])])},function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("thead",[r("tr",[r("th",{attrs:{scope:"col"}},[t._v("Territory")]),r("th",{attrs:{scope:"col"}},[t._v("Publisher")]),r("th",{attrs:{scope:"col"}},[t._v("Checked Out")]),r("th",{attrs:{scope:"col"}},[t._v("Checked In")]),r("th",{attrs:{scope:"col"}})])])}],s=(r("96cf"),r("1da1")),i=r("f2b3"),o=r("b166"),c=r("e3ee"),l=function(){var t=this,e=t.$createElement,r=t._self._c||e;return t.open?r("div",[r("transition",{attrs:{name:"modal"}},[r("div",{staticClass:"modal-mask"},[r("div",{staticClass:"modal-wrapper"},[r("div",{staticClass:"modal-dialog",attrs:{role:"document"}},[t.territory?r("div",{staticClass:"modal-content"},[r("div",{staticClass:"modal-header"},[t.territory.streetTerritoryCode?r("h5",[t._v(" Check In "+t._s(t.territory.streetTerritoryCode)+" / "+t._s(t.territory.territoryCode)+" ")]):r("h5",{attrs:{scope:"row"}},[t._v("Check In "+t._s(t.t.territoryCode))]),r("button",{staticClass:"close",attrs:{type:"button","data-dismiss":"modal","aria-label":"Close"}},[r("span",{attrs:{"aria-hidden":"true"},on:{click:function(e){return e.preventDefault(),t.close(e)}}},[t._v("×")])])]),r("div",{staticClass:"modal-body"},[r("form",[r("div",{staticClass:"form-group"},[r("label",{staticClass:"control-label"},[t._v("Date")]),r("input",{directives:[{name:"model",rawName:"v-model",value:t.checkinDate,expression:"checkinDate"}],staticClass:"form-control",attrs:{type:"datetime-local"},domProps:{value:t.checkinDate},on:{input:function(e){e.target.composing||(t.checkinDate=e.target.value)}}})])])]),r("div",{staticClass:"modal-footer"},[r("button",{staticClass:"btn btn-secondary",attrs:{type:"button"},on:{click:function(e){return e.preventDefault(),t.close(e)}}},[t._v(" Close ")]),r("button",{staticClass:"btn btn-primary",class:{disabled:!t.checkinDate},attrs:{type:"button"},on:{click:function(e){return e.preventDefault(),t.confirm(e)}}},[t._v(" Check In ")])])]):t._e()])])])])],1):t._e()},u=[],d=r("fd3a"),h=r("2420"),p=r("8c86");function v(t){Object(p["a"])(1,arguments);var e=Object(d["a"])(t);return e.setHours(0,0,0,0),e}var m=864e5;function f(t,e){Object(p["a"])(2,arguments);var r=v(t),a=v(e),n=r.getTime()-Object(h["a"])(r),s=a.getTime()-Object(h["a"])(a);return Math.round((n-s)/m)}function b(t,e){var r=t.getFullYear()-e.getFullYear()||t.getMonth()-e.getMonth()||t.getDate()-e.getDate()||t.getHours()-e.getHours()||t.getMinutes()-e.getMinutes()||t.getSeconds()-e.getSeconds()||t.getMilliseconds()-e.getMilliseconds();return r<0?-1:r>0?1:r}function C(t,e){Object(p["a"])(2,arguments);var r=Object(d["a"])(t),a=Object(d["a"])(e),n=b(r,a),s=Math.abs(f(r,a));r.setDate(r.getDate()-n*s);var i=b(r,a)===-n,o=n*(s-i);return 0===o?0:o}var _={name:"AdminTerritoryCheckinModal",props:{territory:{type:Object,default:null},open:{type:Boolean,default:function(){return!1}}},data:function(){return{checkinDate:null}},methods:{close:function(){this.$emit("close")},confirm:function(){var t=Object(c["a"])(this.checkinDate);t&&C(new Date,t)<1&&(t=null),this.$emit("confirm",{territoryId:this.territory.territoryId,checkinDate:t})}},watch:{open:{immediate:!0,handler:function(t){t&&(this.checkinDate=Object(o["a"])(new Date,"yyyy-MM-dd'T'hh:mm"))}}}},y=_,g=(r("1070"),r("2877")),k=Object(g["a"])(y,l,u,!1,null,"7cf18998",null),w=k.exports,x=function(){var t=this,e=t.$createElement,r=t._self._c||e;return t.open?r("div",[r("transition",{attrs:{name:"modal"}},[r("div",{staticClass:"modal-mask"},[r("div",{staticClass:"modal-wrapper"},[r("div",{staticClass:"modal-dialog",attrs:{role:"document"}},[t.territory?r("div",{staticClass:"modal-content"},[r("div",{staticClass:"modal-header"},[t.territory.streetTerritoryCode?r("h5",[t._v(" Check Out "+t._s(t.territory.streetTerritoryCode)+" / "+t._s(t.territory.territoryCode)+" ")]):r("h5",{attrs:{scope:"row"}},[t._v("Check Out "+t._s(t.t.territoryCode))]),r("button",{staticClass:"close",attrs:{type:"button","data-dismiss":"modal","aria-label":"Close"}},[r("span",{attrs:{"aria-hidden":"true"},on:{click:function(e){return e.preventDefault(),t.close(e)}}},[t._v("×")])])]),r("div",{staticClass:"modal-body"},[r("form",[r("div",{staticClass:"form-group"},[r("label",{staticClass:"control-label"},[t._v("Publisher")]),r("input",{directives:[{name:"model",rawName:"v-model",value:t.publisherSearch,expression:"publisherSearch"}],staticClass:"form-control",attrs:{type:"text",autocomplete:"off",placeholder:"enter publisher",list:"availablePublisherList"},domProps:{value:t.publisherSearch},on:{input:function(e){e.target.composing||(t.publisherSearch=e.target.value)}}}),r("datalist",{attrs:{id:"availablePublisherList"}},t._l(t.publisherMatches,(function(e,a){return r("option",{key:a},[t._v(t._s(e.name))])})),0)]),r("div",{staticClass:"form-group"},[r("label",{staticClass:"control-label"},[t._v("Date")]),r("input",{directives:[{name:"model",rawName:"v-model",value:t.checkoutDate,expression:"checkoutDate"}],staticClass:"form-control",attrs:{type:"datetime-local"},domProps:{value:t.checkoutDate},on:{input:function(e){e.target.composing||(t.checkoutDate=e.target.value)}}})])])]),r("div",{staticClass:"modal-footer"},[r("button",{staticClass:"btn btn-secondary",attrs:{type:"button"},on:{click:function(e){return e.preventDefault(),t.close(e)}}},[t._v(" Close ")]),r("button",{staticClass:"btn btn-primary",class:{disabled:!t.checkoutDate||!t.selectedPublisher},attrs:{type:"button"},on:{click:function(e){return e.preventDefault(),t.confirm(e)}}},[t._v(" Check Out ")])])]):t._e()])])])])],1):t._e()},O=[],D=(r("4de4"),r("7db0"),r("c975"),r("b0c0"),r("2909")),T={name:"AdminTerritoryCheckoutModal",props:{territory:{type:Object,default:null},open:{type:Boolean,default:function(){return!1}}},data:function(){return{checkoutDate:null,publisherSearch:"",publisherSearchToken:void 0,availablePublishers:[],selectedPublisher:void 0}},methods:{close:function(){this.$emit("close")},confirm:function(){var t=Object(c["a"])(this.checkoutDate);t&&C(new Date,t)<1&&(t=null),this.$emit("confirm",{territoryId:this.territory.territoryId,publisherId:this.selectedPublisher.id,checkoutDate:t}),this.publisherSearch="",this.publisherSearchToken=void 0,this.availablePublishers=[],this.selectedPublisher=void 0},loadPublishers:function(t){var e=this;return Object(s["a"])(regeneratorRuntime.mark((function r(){var a;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:return r.next=2,i["a"].getPublisherSelectOptions(t);case 2:a=r.sent,e.availablePublishers=Object(D["a"])(a);case 4:case"end":return r.stop()}}),r)})))()}},watch:{open:{immediate:!0,handler:function(t){t&&(this.checkoutDate=Object(o["a"])(new Date,"yyyy-MM-dd'T'hh:mm"))}},publisherSearch:function(t){var e=this;return Object(s["a"])(regeneratorRuntime.mark((function r(){var a;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:if(a=e.availablePublishers.find((function(e){var r=e.name;return r.toLowerCase()===t.toLowerCase()})),e.selectedPublisher=a?Object.assign({},a):void 0,!(t.length<3||e.selectedPublisher)){r.next=4;break}return r.abrupt("return");case 4:if(!e.publisherSearchToken||-1===t.indexOf(e.publisherSearchToken)){r.next=6;break}return r.abrupt("return");case 6:return e.publisherSearchToken=t,r.next=9,e.loadPublishers(t);case 9:case"end":return r.stop()}}),r)})))()}},computed:{publisherMatches:function(){var t=this;return this.availablePublishers.filter((function(e){return e.name.toLowerCase().indexOf(t.publisherSearch.toLowerCase())>=0}))}}},I=T,P=(r("3cae"),Object(g["a"])(I,x,O,!1,null,"61c29830",null)),M=P.exports,j={name:"AdminTerritory",props:{type:{type:String,required:!0}},data:function(){return{territories:[],selectedTerritory:null,isCheckinModalOpen:!1,isCheckoutModalOpen:!1}},components:{AdminTerritoryCheckinModal:w,AdminTerritoryCheckoutModal:M},methods:{loadTerritories:function(){var t=this;return Object(s["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:if(t.territories=[],"street"!==t.type){e.next=7;break}return e.next=4,i["a"].getStreetTerritory();case 4:t.territories=e.sent,e.next=11;break;case 7:if("inaccessible"!==t.type){e.next=11;break}return e.next=10,i["a"].getInaccessibleTerritory();case 10:t.territories=e.sent;case 11:case"end":return e.stop()}}),e)})))()},displayDate:function(t){return Object(o["a"])(Object(c["a"])(t),"MMM dd, yyyy")},checkIn:function(t){this.selectedTerritory=t,this.isCheckinModalOpen=!0},checkOut:function(t){this.selectedTerritory=t,this.isCheckoutModalOpen=!0},checkinConfirm:function(t){var e=this;return Object(s["a"])(regeneratorRuntime.mark((function r(){return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:return r.next=2,i["a"].userCheckin(t.territoryId,t.checkinDate);case 2:return r.next=4,e.loadTerritories();case 4:e.isCheckinModalOpen=!1;case 5:case"end":return r.stop()}}),r)})))()},checkoutConfirm:function(t){var e=this;return Object(s["a"])(regeneratorRuntime.mark((function r(){return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:return r.next=2,i["a"].publisherCheckout(t.territoryId,t.publisherId,t.checkoutDate);case 2:return r.next=4,e.loadTerritories();case 4:e.isCheckoutModalOpen=!1;case 5:case"end":return r.stop()}}),r)})))()}},watch:{type:{immediate:!0,handler:function(){var t=this;return Object(s["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return e.next=2,t.loadTerritories();case 2:case"end":return e.stop()}}),e)})))()}},isCheckinModalOpen:{handler:function(t){t&&(this.isCheckoutModalOpen=!t)}},isCheckoutModalOpen:{handler:function(t){t&&(this.isCheckinModalOpen=!t)}}}},E=j,A=Object(g["a"])(E,a,n,!1,null,null,null);e["default"]=A.exports},db8d:function(t,e,r){"use strict";r.r(e);var a=function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",[r("div",{staticClass:"d-flex"},[r("div",{staticClass:"flex-grow-1 mb-2"}),t._m(0)]),t._l(t.properties,(function(e){return r("div",{key:"card"+e.inaccessiblePropertyId,class:{card:!0}},[r("ul",{staticClass:"bg-light list-group list-group-flush"},[r("a",{staticClass:"d-flex justify-content-between list-group-item list-group-item-action",attrs:{href:"#"},on:{click:function(r){return r.preventDefault(),t.toggle(e)}}},[r("div",[r("strong",[t._v(t._s(e.streetNumbers)+" "+t._s(e.street)),e.propertyName?[t._v(" ("+t._s(e.propertyName)+")")]:t._e()],2)]),r("div",[r("span",{staticClass:"badge badge-secondary mr-2"},[t._v(t._s(e.contacts.length))]),r("i",{class:{arrow:!0,down:!e.isExpanded,up:e.isExpanded}})])])]),r("div",{class:{"card-body":!0,collapse:!0,show:e.isExpanded}},[0!==e.errors.length?r("div",{staticClass:"alert alert-danger",attrs:{role:"alert"}},[r("ul",t._l(e.errors,(function(e,a){return r("li",{key:a},[t._v(t._s(e))])})),0)]):t._e(),0!==e.warnings.length?r("div",{staticClass:"alert alert-warning",attrs:{role:"alert"}},[r("ul",t._l(e.warnings,(function(e,a){return r("li",{key:a},[t._v(t._s(e))])})),0)]):t._e(),0===e.fileContacts.length&&0===e.contacts.length?r("form",[r("div",{staticClass:"form-group"},[r("label",{attrs:{for:"fileInput"+e.inaccessiblePropertyId}},[t._v("Import File")]),r("input",{staticClass:"form-control-file",attrs:{type:"file",accept:".csv",id:"fileInput"+e.inaccessiblePropertyId},on:{change:function(r){return t.changeFile(r,e.inaccessiblePropertyId)}}})]),e.file?r("a",{staticClass:"btn btn-primary",attrs:{href:"#",role:"button"},on:{click:function(r){return r.preventDefault(),t.uploadFile(e.inaccessiblePropertyId)}}},[t._v("Upload")]):t._e()]):r("table",{staticClass:"table table-bordered"},[t._m(1,!0),r("tbody",[t._l(e.fileContacts,(function(e,a){return[r("tr",{key:"row-contact-"+a},[r("td",[t._v(" "+t._s(e.firstName)+" ")]),r("td",[t._v(" "+t._s(e.lastName)+" ")]),r("td",[t._v(" "+t._s(e.middleInitial)+" ")]),r("td",[t._v(" "+t._s(e.age)+" ")]),r("td",[t._v(" "+t._s(e.mailingAddress1)+" ")]),r("td",[t._v(" "+t._s(e.mailingAddress2)+" ")]),r("td",[t._v(" "+t._s(e.postalCode)+" ")]),r("td",[t._v(" "+t._s(e.phoneNumber)+" ")]),r("td",[t._v(" "+t._s(e.phoneType)+" ")])]),0!==e.errors.length||0!==e.warnings.length?r("tr",{key:"row-message-"+a},[r("td",{attrs:{colspan:"9"}},[0!==e.errors.length?r("div",{staticClass:"alert alert-danger",attrs:{role:"alert"}},[r("h5",{staticClass:"alert-heading"},[t._v("Errors")]),r("ul",t._l(e.errors,(function(e,n){return r("li",{key:"err-"+a+"-"+n},[t._v(t._s(e))])})),0)]):t._e(),0!==e.warnings.length?r("div",{staticClass:"alert alert-warning",attrs:{role:"alert"}},[r("h5",{staticClass:"alert-warning"},[t._v("Warnings")]),r("ul",t._l(e.warnings,(function(e,n){return r("li",{key:"warn-"+a+"-"+n},[t._v(t._s(e))])})),0)]):t._e()])]):t._e()]})),t._l(e.contacts,(function(e,a){return r("tr",{key:a},[r("td",[t._v(" "+t._s(e.firstName)+" ")]),r("td",[t._v(" "+t._s(e.lastName)+" ")]),r("td",[t._v(" "+t._s(e.middleInitial)+" ")]),r("td",[t._v(" "+t._s(e.age)+" ")]),r("td",[t._v(" "+t._s(e.mailingAddress1)+" ")]),r("td",[t._v(" "+t._s(e.mailingAddress2)+" ")]),r("td",[t._v(" "+t._s(e.postalCode)+" ")]),r("td",[t._v(" "+t._s(e.phoneNumber)+" ")]),r("td",[e.phoneType?[t._v(t._s(e.phoneType.name))]:t._e()],2)])}))],2)]),0!==e.fileContacts.length||0===e.contacts.length||e.removeContacts?t._e():r("button",{staticClass:"btn btn-primary",attrs:{type:"button"},on:{click:function(t){t.preventDefault(),e.removeContacts=!0}}},[t._v("Remove Contacts")]),0===e.fileContacts.length&&0!==e.contacts.length&&e.removeContacts?r("div",{staticClass:"alert alert-warning",attrs:{role:"alert"}},[r("h5",{staticClass:"alert-heading"},[t._v("Remove Contacts")]),r("p",[t._v("Are you sure you want to remove these "+t._s(e.contacts.length)+" contacts from this property?")]),r("hr"),r("button",{staticClass:"btn btn-secondary btn-sm mr-1",attrs:{type:"button"},on:{click:function(t){t.preventDefault(),e.removeContacts=!1}}},[t._v("Cancel")]),r("button",{staticClass:"btn btn-primary btn-sm",attrs:{type:"button"},on:{click:function(r){return r.preventDefault(),t.removeContactsConfirm(e.inaccessiblePropertyId)}}},[t._v("Confirm")])]):t._e(),e.fileContacts.length>e.rowErrors?r("div",{class:{alert:!0,"alert-success":!(e.rowErrors|e.rowWarnings),"alert-warning":e.rowErrors|e.rowWarnings},attrs:{role:"alert"}},[r("h5",{staticClass:"alert-heading"},[t._v("Please Review...")]),e.rowErrors|e.rowWarnings?r("p",[t._v("There are "+t._s(e.fileContacts.length)+" contacts in this file. "+t._s(e.rowErrors)+" contacts have errors and will not be imported. "+t._s(e.rowWarnings)+" contacts with warnings have some information that cannot be imported. Are you sure you want to import these contacts?")]):r("p",[t._v("There are "+t._s(e.fileContacts.length)+" contacts in this file. Are you ready to import these contacts?")]),r("hr"),r("button",{staticClass:"btn btn-secondary btn-sm mr-1",attrs:{type:"button"},on:{click:function(r){return r.preventDefault(),t.fileContactCancel(e.inaccessiblePropertyId)}}},[t._v("Cancel")]),r("button",{staticClass:"btn btn-primary btn-sm",attrs:{type:"button"},on:{click:function(r){return r.preventDefault(),t.fileContactConfirm(e.inaccessiblePropertyId)}}},[t._v("Confirm")])]):t._e()])])}))],2)},n=[function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("div",{staticClass:"flex-grow-1 flex-md-grow-0 mb-2"},[r("button",{staticClass:"close",attrs:{type:"button","aria-label":"Close"}},[r("span",{attrs:{"aria-hidden":"true"}},[t._v("×")])])])},function(){var t=this,e=t.$createElement,r=t._self._c||e;return r("thead",[r("tr",[r("th",[t._v(" First Name ")]),r("th",[t._v(" Last Name ")]),r("th",[t._v(" Middle Initial ")]),r("th",[t._v(" Age ")]),r("th",[t._v(" Address 1 ")]),r("th",[t._v(" Address 2 ")]),r("th",[t._v(" Postal Code ")]),r("th",[t._v(" Phone ")]),r("th",[t._v(" Phone Type ")])])])}],s=(r("7db0"),r("4160"),r("a9e3"),r("159b"),r("2909")),i=(r("96cf"),r("1da1")),o=r("f2b3"),c={name:"AdminContactsImport",props:{id:{type:Number,default:0}},data:function(){return{properties:[]}},created:function(){var t=this;return Object(i["a"])(regeneratorRuntime.mark((function e(){return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return e.next=2,t.loadProperties();case 2:case"end":return e.stop()}}),e)})))()},methods:{loadProperties:function(){var t=this;return Object(i["a"])(regeneratorRuntime.mark((function e(){var r;return regeneratorRuntime.wrap((function(e){while(1)switch(e.prev=e.next){case 0:return e.next=2,o["a"].getTerritoryProperties(t.id);case 2:r=e.sent,r.forEach((function(t){t.isExpanded=!1,t.errors=[],t.warnings=[],t.rowErrors=0,t.rowWarnings=0,t.file=null,t.fileContacts=[],t.removeContacts=!1,t.contacts=t.contacts?t.contacts:[]})),t.properties=Object(s["a"])(r);case 5:case"end":return e.stop()}}),e)})))()},toggle:function(t){t.isExpanded=!t.isExpanded},changeFile:function(t,e){var r=this.properties.find((function(t){return t.inaccessiblePropertyId===e}));r.file=0!==t.target.files.length?t.target.files[0]:null},uploadFile:function(t){var e=this;return Object(i["a"])(regeneratorRuntime.mark((function r(){var a,n;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:return a=e.properties.find((function(e){return e.inaccessiblePropertyId===t})),r.next=3,o["a"].uploadContactsCsv(a.file);case 3:n=r.sent,a.errors=n.errors,a.warnings=n.warnings,a.rowErrors=n.rowErrors,a.rowWarnings=n.rowWarnings,a.fileContacts=n.rows;case 9:case"end":return r.stop()}}),r)})))()},fileContactCancel:function(t){var e=this.properties.find((function(e){return e.inaccessiblePropertyId===t}));e.errors=[],e.warnings=[],e.rowErrors=0,e.rowWarnings=0,e.file=null,e.fileContacts=[]},fileContactConfirm:function(t){var e=this;return Object(i["a"])(regeneratorRuntime.mark((function r(){var a,n;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:return a=e.properties.find((function(e){return e.inaccessiblePropertyId===t})),r.next=3,o["a"].uploadContacts(t,a.fileContacts);case 3:n=r.sent,a.errors=[],a.warnings=[],a.rowErrors=0,a.rowWarnings=0,a.file=null,a.fileContacts=[],a.contacts=n;case 11:case"end":return r.stop()}}),r)})))()},removeContactsConfirm:function(t){var e=this;return Object(i["a"])(regeneratorRuntime.mark((function r(){var a;return regeneratorRuntime.wrap((function(r){while(1)switch(r.prev=r.next){case 0:return a=e.properties.find((function(e){return e.inaccessiblePropertyId===t})),r.next=3,o["a"].removePropertyContactList(t);case 3:a.errors=[],a.warnings=[],a.rowErrors=0,a.rowWarnings=0,a.file=null,a.fileContacts=[],a.contacts=[],a.removeContacts=!1;case 11:case"end":return r.stop()}}),r)})))()}}},l=c,u=(r("50ed"),r("2877")),d=Object(u["a"])(l,a,n,!1,null,"d30bb636",null);e["default"]=d.exports}}]);
//# sourceMappingURL=chunk-admin.js.map