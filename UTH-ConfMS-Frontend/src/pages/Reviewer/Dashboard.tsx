<<<<<<< HEAD
import React, { useState } from 'react';
=======

import React, { useState, useEffect } from 'react';
>>>>>>> a182007c4692c7c2c315091b1cea53f95128d224
import { PDFPreview } from '../../components/PDFPreview';
import { AIBadge } from '../../components/AIBadge';
import { ReviewForm } from '../../components/ReviewForm';

export const ReviewerDashboard: React.FC = () => {
  const [selectedPaper, setSelectedPaper] = useState<number | null>(null);

  // State quản lý form đánh giá
  const [reviewData, setReviewData] = useState({
    novelty: 5,
    methodology: 4,
    presentation: 3,
    comments: '',
    confidentialComments: '',
    decision: 'Accept'
  });

  // Reset form khi chuyển bài báo
  useEffect(() => {
    if (selectedPaper) {
      setReviewData({
        novelty: 5,
        methodology: 4,
        presentation: 3,
        comments: '',
        confidentialComments: '',
        decision: 'Accept'
      });
    }
  }, [selectedPaper]);

  const handleInputChange = (field: string, value: any) => {
    setReviewData(prev => ({ ...prev, [field]: value }));
  };

  const handleSubmit = () => {
    console.log("Submitting review:", { paperId: selectedPaper, ...reviewData });
    // TODO: Gọi API submit review tại đây
    alert("Đã gửi đánh giá thành công!");
    setSelectedPaper(null);
  };

  return (
    <div className="w-full bg-background-light dark:bg-background-dark py-8 px-5 md:px-10 flex justify-center">
        <div className="w-full max-w-[1200px] grid grid-cols-1 lg:grid-cols-3 gap-6">
            
            {/* Paper List */}
            <div className={`${selectedPaper ? 'lg:col-span-1' : 'lg:col-span-3'} flex flex-col gap-4 transition-all`}>
                <h1 className="text-2xl font-bold text-text-main-light dark:text-text-main-dark mb-2">Bài được phân công</h1>
                {[1, 2, 3].map((id) => (
                    <div 
                        key={id} 
                        onClick={() => setSelectedPaper(id)}
                        className={`bg-white dark:bg-card-dark p-5 rounded-xl border cursor-pointer transition-all hover:shadow-md ${selectedPaper === id ? 'border-primary ring-1 ring-primary' : 'border-border-light'}`}
                    >
                        <div className="flex justify-between items-start mb-2">
                            <span className="text-xs font-mono bg-gray-100 px-2 py-1 rounded">#{100 + id}</span>
                            <span className="text-xs font-bold text-red-500">Hạn: 25/06</span>
                        </div>
                        <h3 className="font-bold text-sm mb-2">Deep Learning Approaches for Traffic Flow Prediction in Smart Cities</h3>
                        <p className="text-xs text-text-sec-light line-clamp-2">This paper proposes a novel hybrid architecture combining CNN and LSTM to predict traffic flow with high accuracy...</p>
                        <div className="mt-3 flex items-center gap-2 flex-wrap">
                            <span className="text-xs px-2 py-1 bg-blue-50 text-blue-700 rounded-full font-medium">AI & Big Data</span>
                            <span className="text-xs px-2 py-1 bg-yellow-50 text-yellow-700 rounded-full font-medium">Chờ phản biện</span>
                            {id === 1 && <AIBadge label="High Match" size="sm" />}
                        </div>
                    </div>
                ))}
            </div>

            {/* Review Form (Visible when selected) */}
            {selectedPaper && (
                <div className="lg:col-span-2 bg-white dark:bg-card-dark rounded-xl border border-border-light shadow-lg flex flex-col h-[800px] overflow-hidden">
                    <div className="p-4 border-b border-border-light flex justify-between items-center bg-gray-50 dark:bg-gray-800">
                        <h3 className="font-bold">Đánh giá bài báo #{100 + selectedPaper}</h3>
                        <button onClick={() => setSelectedPaper(null)} className="text-gray-400 hover:text-gray-600 material-symbols-outlined">close</button>
                    </div>
                    <div className="flex-1 overflow-y-auto p-6">
                        <div className="flex flex-col gap-6">
                            
                            {/* PDF Preview Integration */}
                            <div className="h-64 w-full">
                                <PDFPreview fileName={`paper_${100 + selectedPaper}_v1.pdf`} />
                            </div>

<<<<<<< HEAD
                            {/* Gọi Component ReviewForm tại đây */}
                            <ReviewForm paperId={selectedPaper} />
                        </div>
                    </div>
=======
                            <div className="space-y-4">
                                <h4 className="font-bold border-b pb-1">Tiêu chí đánh giá</h4>
                                <div className="grid grid-cols-2 gap-4 items-center">
                                    <label className="text-sm">Tính mới & Đóng góp</label>
                                    <select 
                                        className="text-sm border rounded p-1"
                                        value={reviewData.novelty}
                                        onChange={(e) => handleInputChange('novelty', parseInt(e.target.value))}
                                    >
                                        <option value={5}>5 - Xuất sắc</option>
                                        <option value={4}>4 - Tốt</option>
                                        <option value={3}>3 - Khá</option>
                                        <option value={2}>2 - Trung bình</option>
                                        <option value={1}>1 - Kém</option>
                                    </select>
                                    
                                    <label className="text-sm">Phương pháp nghiên cứu</label>
                                    <select 
                                        className="text-sm border rounded p-1"
                                        value={reviewData.methodology}
                                        onChange={(e) => handleInputChange('methodology', parseInt(e.target.value))}
                                    >
                                        <option value={5}>5 - Xuất sắc</option>
                                        <option value={4}>4 - Tốt</option>
                                        <option value={3}>3 - Khá</option>
                                        <option value={2}>2 - Trung bình</option>
                                        <option value={1}>1 - Kém</option>
                                    </select>
                                    
                                    <label className="text-sm">Trình bày & Ngôn ngữ</label>
                                    <select 
                                        className="text-sm border rounded p-1"
                                        value={reviewData.presentation}
                                        onChange={(e) => handleInputChange('presentation', parseInt(e.target.value))}
                                    >
                                        <option value={5}>5 - Xuất sắc</option>
                                        <option value={4}>4 - Tốt</option>
                                        <option value={3}>3 - Khá</option>
                                        <option value={2}>2 - Trung bình</option>
                                        <option value={1}>1 - Kém</option>
                                    </select>
                                </div>
                            </div>

                            <div className="space-y-2">
                                <h4 className="font-bold">Nhận xét chi tiết cho tác giả</h4>
                                <textarea 
                                    className="w-full h-32 p-3 border rounded text-sm resize-none" 
                                    placeholder="Điểm mạnh, điểm yếu, đề xuất chỉnh sửa..."
                                    value={reviewData.comments}
                                    onChange={(e) => handleInputChange('comments', e.target.value)}
                                ></textarea>
                            </div>

                            <div className="space-y-2">
                                <h4 className="font-bold">Nhận xét bí mật cho Ban chương trình</h4>
                                <textarea 
                                    className="w-full h-20 p-3 border rounded text-sm resize-none bg-yellow-50" 
                                    placeholder="Chỉ ban chương trình mới thấy nội dung này..."
                                    value={reviewData.confidentialComments}
                                    onChange={(e) => handleInputChange('confidentialComments', e.target.value)}
                                ></textarea>
                            </div>
                            
                            <div className="space-y-2">
                                <h4 className="font-bold">Quyết định đề xuất</h4>
                                <div className="flex gap-4">
                                    <label className="flex items-center gap-2 text-sm">
                                        <input 
                                            type="radio" 
                                            name="decision" 
                                            value="Accept"
                                            checked={reviewData.decision === 'Accept'}
                                            onChange={(e) => handleInputChange('decision', e.target.value)}
                                            className="text-green-600"
                                        /> Accept
                                    </label>
                                    <label className="flex items-center gap-2 text-sm">
                                        <input 
                                            type="radio" 
                                            name="decision" 
                                            value="Minor Revision"
                                            checked={reviewData.decision === 'Minor Revision'}
                                            onChange={(e) => handleInputChange('decision', e.target.value)}
                                            className="text-yellow-600"
                                        /> Minor Revision
                                    </label>
                                    <label className="flex items-center gap-2 text-sm">
                                        <input 
                                            type="radio" 
                                            name="decision" 
                                            value="Reject"
                                            checked={reviewData.decision === 'Reject'}
                                            onChange={(e) => handleInputChange('decision', e.target.value)}
                                            className="text-red-600"
                                        /> Reject
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="p-4 border-t border-border-light bg-gray-50 flex justify-end gap-3">
                        <button className="px-4 py-2 rounded text-sm font-medium hover:bg-gray-200">Lưu nháp</button>
                        <button onClick={handleSubmit} className="px-4 py-2 rounded bg-primary text-white text-sm font-bold hover:bg-primary-hover shadow-sm">Gửi đánh giá</button>
                    </div>
>>>>>>> a182007c4692c7c2c315091b1cea53f95128d224
                </div>
            )}

        </div>
    </div>
  );
};