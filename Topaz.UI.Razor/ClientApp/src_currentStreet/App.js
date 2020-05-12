import React from "react";
import { render } from "react-dom";
import IndexPage from "./components/IndexPage";
import CheckOutPage from "./components/CheckOutPage";
import CheckInPage from "./components/CheckInPage";
import ReworkPage from "./components/ReworkPage";
import NotFoundPage from "./components/NotFoundPage";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

render(
  <Router basename={"/CurrentStreet"}>
    <div className="container-fluid">
      <Switch>
        <Route path="/" exact component={IndexPage}></Route>
        <Route path="/checkout" component={CheckOutPage}></Route>
        <Route path="/checkin/:id" component={CheckInPage}></Route>
        <Route path="/rework/:id" component={ReworkPage}></Route>
        <Route component={NotFoundPage}></Route>
      </Switch>
    </div>
  </Router>,
  document.getElementById("app")
);
