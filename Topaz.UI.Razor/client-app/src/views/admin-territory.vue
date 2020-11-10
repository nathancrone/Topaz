<template>
  <div>
    <div class="row mt-3">
      <ul class="nav nav-tabs flex-column flex-md-row">
        <li class="nav-item">
          <a
            class="nav-link active"
            href="#">Street</a>
        </li>
        <li class="nav-item">
          <a
            class="nav-link"
            href="#">Inaccessible</a>
        </li>
      </ul>
    </div>
    <div>
 <div class="row no-gutters">
          <div class="col">
            <h3>Territories</h3>
            <table class="table table-sm">
              <thead>
                <tr>
                  <th scope="col">Territory</th>
                  <th scope="col">Publisher</th>
                  <th scope="col">Checked Out</th>
                  <th scope="col">Checked In</th>
                  <th scope="col">&nbsp;</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <th scope="row">N-008</th>
                  <td>Nathan Crone</td>
                  <td>10/10/2020</td>
                  <td>11/10/2020</td>
                  <td>
                    <a class="btn btn-sm btn-primary mr-1" href="#">check out</a>
                    <a class="btn btn-sm btn-primary" href="#">history</a>
                  </td>
                </tr>
                <tr>
                  <th scope="row">S-021</th>
                  <td>Beverly Cofer</td>
                  <td>11/10/2020</td>
                  <td>-</td>
                  <td>
                    <a class="btn btn-sm btn-primary mr-1" href="#">check in</a>
                    <a class="btn btn-sm btn-primary" href="#">history</a>
                  </td>
                </tr>
                <tr>
                  <th scope="row">N-010</th>
                  <td colspan="3" class="text-center">-</td>
                  <td><a class="btn btn-sm btn-primary" href="#">check out</a></td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AdminTerritory",
  props: {
    type: {
      type: String,
      default: 'street',
    },
  },
  data() {
    return {};
  },
};
</script>
