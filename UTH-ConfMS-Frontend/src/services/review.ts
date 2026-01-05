import { api } from './api';

export interface ReviewSubmission {
  paperId: number;
  score: number;       // Ví dụ: thang 1-10 hoặc -3 đến 3
  comments: string;    // Nhận xét chi tiết
  confidence: number;  // Độ tự tin của reviewer
  recommendation: 'Accept' | 'Reject' | 'Revision';
}

export interface ReviewResponse {
  id: number;
  reviewerName: string;
  score: number;
  comments: string;
  submitDate: string;
}

export const reviewApi = {
  // 1. Lấy danh sách bài báo ĐƯỢC PHÂN CÔNG cho tôi (Reviewer)
  getAssignedPapers: async () => {
    return api.get<any[]>('/reviews/assigned-to-me');
  },

  // 2. Reviewer gửi kết quả đánh giá
  submitReview: async (data: ReviewSubmission) => {
    return api.post('/reviews', data);
  },

  // 3. (Dành cho Chair/Author) Xem danh sách review của một bài báo
  getReviewsByPaperId: async (paperId: number) => {
    return api.get<ReviewResponse[]>(`/reviews/paper/${paperId}`);
  },

  // 4. Cập nhật review (nếu còn hạn)
  updateReview: async (reviewId: number, data: Partial<ReviewSubmission>) => {
    return api.put(`/reviews/${reviewId}`, data);
  }
};