import axios from 'axios';

const apiClient = axios.create({
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Thêm token nếu có
apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

export const api = {
  get: (url: string) => apiClient.get(url),
  post: (url: string, data: any) => apiClient.post(url, data),
  put: (url: string, data: any) => apiClient.put(url, data),
  delete: (url: string) => apiClient.delete(url),
};

export const aiApi = {
  checkGrammar: async (text: string) => {
    // Gateway map /api/ai -> ai-service
    return apiClient.post('/ai/check-grammar', { text });
  }
};