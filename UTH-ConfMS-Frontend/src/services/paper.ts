import apiClient, { ApiResponse, PagedResponse } from './apiClient';

export interface AuthorSubmission {
    fullName: string;
    email: string;
    affiliation: string;
    isCorresponding: boolean;
    order: number;
}

export interface PaperSubmission {
  title: string;
  abstract: string;
  keywords: string[];
  conferenceId: string;
  topicId?: number;
  authors: AuthorSubmission[];
  file: File;
}

export interface PaperResponse {
  id: number;
  title: string;
  abstract: string;
  status: string; // 'Submitted', 'UnderReview', 'Accepted', 'Rejected'
  trackName?: string;
  submissionDate: string;
  fileName?: string;
}

export const paperApi = {
  // 1. Nộp bài báo mới (Sử dụng FormData để gửi file)
  submitPaper: async (data: PaperSubmission) => {
    const formData = new FormData();
    formData.append('title', data.title);
    formData.append('abstract', data.abstract);
    formData.append('conferenceId', data.conferenceId.toString());
    if (data.topicId) formData.append('topicId', data.topicId.toString());
    
    // Xử lý mảng keywords
    data.keywords.forEach((k) => {
        formData.append('keywords', k);
    });

    // Xử lý mảng authors
    data.authors.forEach((author, index) => {
      formData.append(`authors[${index}].fullName`, author.fullName);
      formData.append(`authors[${index}].email`, author.email);
      formData.append(`authors[${index}].affiliation`, author.affiliation || '');
      formData.append(`authors[${index}].isCorresponding`, author.isCorresponding.toString());
      formData.append(`authors[${index}].order`, author.order.toString());
    });

    formData.append('file', data.file);

    const response = await apiClient.post<ApiResponse<PaperResponse>>('/api/submissions', formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    });
    return response.data;
  },

  // 2. Lấy danh sách bài báo của người đang đăng nhập
  getMyPapers: async () => {
    const response = await apiClient.get<ApiResponse<PagedResponse<PaperResponse>>>('/api/submissions/my-submissions');
    return response.data?.data?.items || []; // Backend wrapper ApiResponse -> PagedResponse -> Items
  },

  // 3. Lấy chi tiết một bài báo theo ID
  getPaperDetail: async (id: number) => {
    const response = await apiClient.get<ApiResponse<PaperResponse>>(`/api/submissions/${id}`);
    return response.data;
  },

  // 4. Tải file bài báo (Response dạng Blob để trình duyệt tải về)
  downloadPaper: async (id: number) => {
    return apiClient.get(`/api/submissions/${id}/download`, {
      responseType: 'blob', // Quan trọng: báo axios nhận binary data
    });
  },
  
  // 5. Cập nhật bài báo (Re-submit)
  updatePaper: async (id: number, data: Partial<PaperSubmission>) => {
     const formData = new FormData();
     if(data.title) formData.append('title', data.title);
     if(data.abstract) formData.append('abstract', data.abstract);
     if(data.keywords) {
         data.keywords.forEach(k => formData.append('keywords', k));
     }
     if(data.file) formData.append('file', data.file);
     
     const response = await apiClient.put<ApiResponse<PaperResponse>>(`/api/submissions/${id}`, formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
     });
     return response.data;
  },

  // 6. Rút bài báo
  withdrawPaper: async (id: number, reason: string) => {
      const response = await apiClient.post<ApiResponse<void>>(`/api/submissions/${id}/withdraw`, { reason });
      return response.data;
  }
};