import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import fs from 'fs';
import path from 'path';
import { fileURLToPath } from 'url';
import { dirname } from 'path';

// https://vitejs.dev/config/


const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    https: {
      key: fs.readFileSync(path.resolve(__dirname, 'localhost-key.pem')),
      cert: fs.readFileSync(path.resolve(__dirname, 'localhost-cert.pem')),
    },
    proxy: {
      '/api': {
        target: 'https://localhost:8443', // A backend címe HTTPS-en 7246 / 8443
        changeOrigin: true,
        secure: false, //saját aláírású tanúsítványt használsz, állítsd false-ra
      },
    },
  },
});

// https://localhost:7246
// 'http://127.0.0.1:5031'

// export default defineConfig({
//   plugins: [react()],
//   server: {
//     proxy: {
//       '/api': {
//         target: 'https://localhost:7246',
//         changeOrigin: true,
//         secure: false,
//       },
//     },
//   },
// })