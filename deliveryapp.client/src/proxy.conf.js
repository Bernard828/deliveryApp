const { env } = require('process');

const httpsPort = env.ASPNETCORE_HTTPS_PORT || '7299';
const target = `https://127.00.1:${httpsPort}`;
//let target = 'https://127.0.0.1:7299';
//if (env.ASPNETCORE + ASPNETCORE_HTTPS_PORT) {
//  target = `https://127.0.0.1:${env.ASPNETCORE_HTTPS_PORT}`;
//} else if (env.ASPNETCORE_URLS) {
//  const firstUrl = env.ASPNETCORE_URLS.split(';')[0];
//  target = firstUrl.replace('localhost', '127.0.0.1').replace('[::1]', '127.0.0.1');
//}

const PROXY_CONFIG = [
  {
    context: [
      "/api",
      "/User",
      "/Restaurant",
      "/MenuItem",
      "/Orders"
    ],
    target: target,
    secure: false,
    changeOrigin: true,
    logLevel:'debug'
  }
];

module.exports = PROXY_CONFIG;
