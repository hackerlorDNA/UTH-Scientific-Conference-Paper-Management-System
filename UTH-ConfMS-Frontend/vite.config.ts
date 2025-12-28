import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: true, // Cho phép Docker map port
    port: 3000,
    watch: {
      usePolling: true // Bắt buộc để Docker trên Windows nhận diện thay đổi file
    },
    proxy: {
      // Bất cứ request nào bắt đầu bằng /api sẽ được chuyển hướng
      '/api': {
        target: 'http://backend:8080', // Tên service backend trong docker-compose
        changeOrigin: true,
        secure: false,
      }
    }
  }
})