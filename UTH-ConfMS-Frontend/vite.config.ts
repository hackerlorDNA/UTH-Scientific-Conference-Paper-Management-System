import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    host: true, // Cho phép Docker map port ra ngoài
    port: 3000,
    strictPort: true,
    hmr: {
      clientPort: 3000, // Ép WebSocket chạy đúng port 3000
    },
    watch: {
      usePolling: true,
    }
  }
})