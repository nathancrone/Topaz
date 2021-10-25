module.exports = {
  outputDir: "../wwwroot/client-app/",
  filenameHashing: true,
  publicPath: "/client-app/",
  runtimeCompiler: true, 
  css: {
    extract: {
      filename: 'css/[name].css', 
      chunkFilename: 'css/[name].css'
    }
  }
};
