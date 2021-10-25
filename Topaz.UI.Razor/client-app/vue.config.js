module.exports = {
  outputDir: "../wwwroot/client-app/",
  filenameHashing: true,
  publicPath: "/client-app/",
<<<<<<< HEAD
  runtimeCompiler: true, 
  css: {
    extract: {
      filename: 'css/[name].css', 
      chunkFilename: 'css/[name].css'
    }
  }
=======
  runtimeCompiler: true,
  configureWebpack: (config) => {
    config.output.chunkFilename = 'js/[name].[hash:8].js';
  },
>>>>>>> 296563f6e0de636f0a2ad5b3a8ccc00225800d2b
};
