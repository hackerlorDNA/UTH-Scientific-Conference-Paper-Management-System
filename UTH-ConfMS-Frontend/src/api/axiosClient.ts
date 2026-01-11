import axios from 'axios';

const axiosClient = axios.create({
  // ⚠️ QUAN TRỌNG: Phải trỏ về cổng 5044 (Gateway)
  baseURL: 'http://localhost:5044/api', 
  headers: {
    'Content-Type': 'application/json',
  },
});

// Tự động gắn Token vào mỗi yêu cầu gửi đi
axiosClient.interceptors.request.use(async (config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
});

export default axiosClient;