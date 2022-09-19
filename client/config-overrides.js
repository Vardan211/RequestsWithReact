module.exports = function override(config, env) {
  if (env !== "production") {
    return config;
  }

  // Get rid of hash for js files
  // eslint-disable-next-line no-param-reassign
  config.output.filename = "static/js/main.production.js";
  // eslint-disable-next-line no-param-reassign
  config.output.chunkFilename = "static/js/main.production.chunk.js";

  // Get rid of hash for css files
  const miniCssExtractPlugin = config.plugins.find(
    (element) => element.constructor.name === "MiniCssExtractPlugin",
  );
  miniCssExtractPlugin.options.filename = "static/css/main.production.css";
  miniCssExtractPlugin.options.chunkFilename = "static/css/main.production.css";

  return config;
};
