export function shortDate(x) {
  var y = x.split(/\D+/);
  return new Intl.DateTimeFormat("en-US").format(
    new Date(Date.UTC(y[0], --y[1], y[2], y[3], y[4], y[5], y[6]))
  );
}
