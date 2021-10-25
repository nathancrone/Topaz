module.exports = {
  outputDir: "../wwwroot/client-app/",
  filenameHashing: false,
  publicPath: "/client-app/",
  runtimeCompiler: true,
  configureWebpack: (config) => {
    config.output.chunkFilename = 'js/[name].[hash:8].js';
  },
};
