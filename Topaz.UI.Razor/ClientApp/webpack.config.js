const path = require("path");

process.env.NODE_ENV = "development";

module.exports = {
  entry: {
    myInaccessible: "./src_myInaccessible/App.js",
    myStreet: "./src_myStreet/App.js",
  },
  output: {
    path: path.resolve(__dirname, "../wwwroot/js/"),
    filename: "bundle.[name].js",
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /(node_modules)/,
        use: {
          loader: "babel-loader",
          options: {
            presets: ["@babel/preset-react", "@babel/preset-env"],
          },
        },
      },
    ],
  },
  optimization: {
    splitChunks: {
      cacheGroups: {
        vendor: {
          test: /[\\/]node_modules[\\/](react|react-dom|react-router-dom)[\\/]/,
          name: "vendor",
          chunks: "all",
        },
      },
    },
  },
};
