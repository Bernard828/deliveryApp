const { env } = require('process');

// Correct HTTPS port for ASP.NET debugger
const httpsPort = env.ASPNETCORE_HTTPS_PORT || '7140';

// Correct target for HTTPS debugger
const target = `https://localhost:${httpsPort}`;

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
    logLevel: 'debug'
  }
];

module.exports = PROXY_CONFIG;
