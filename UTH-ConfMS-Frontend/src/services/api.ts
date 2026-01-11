import axios from 'axios';

// 1. Cấu hình baseURL trỏ về API Gateway (port 5000)
export const apiClient = axios.create({
  baseURL: (import.meta.env.VITE_API_URL || 'http://localhost:5000') + '/api', 
  headers: {
    'Content-Type': 'application/json',
  },
});

// Thêm token vào header nếu có
apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

export const api = {
  get: <T>(url: string) => apiClient.get<T>(url),
  post: <T>(url: string, data: any) => apiClient.post<T>(url, data),
  put: <T>(url: string, data: any) => apiClient.put<T>(url, data),
  delete: <T>(url: string) => apiClient.delete<T>(url),
};

// 2. Thêm Auth API để gọi xuống AuthController của BE
export const authApi = {
  login: async (credentials: { email: string; password: string }) => {
    // Gọi vào endpoint [HttpPost("login")] của AuthController
    return apiClient.post('/auth/login', credentials);
  },
  register: async (data: any) => {
    return apiClient.post('/auth/register', data);
  }
};

export const aiApi = {
  checkGrammar: async (text: string) => {
    return apiClient.post('/ai/check-grammar', { text });
  }
};