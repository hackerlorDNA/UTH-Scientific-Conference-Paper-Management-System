import axios from 'axios';

export const apiClient = axios.create({
  baseURL: (import.meta.env.VITE_API_URL ||'http://localhost:8080/') + '/api', 
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
  get: <T>(url: string) => apiClient.get<T>(url),
  post: <T>(url: string, data: any) => apiClient.post<T>(url, data),
  put: <T>(url: string, data: any) => apiClient.put<T>(url, data),
  delete: <T>(url: string) => apiClient.delete<T>(url),
};

export const aiApi = {
  checkGrammar: async (text: string) => {
    // Gateway map /api/ai -> ai-service
    return apiClient.post('/ai/check-grammar', { text });
  }
};

export const mockFetch = <T>(data: T, delay = 1000): Promise<T> => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(data);
    }, delay);
  });
};