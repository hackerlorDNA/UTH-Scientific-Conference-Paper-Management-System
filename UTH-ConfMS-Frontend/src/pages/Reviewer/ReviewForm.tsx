import React, { useState } from 'react';

interface ReviewFormProps {
  paperId: string;
  onNavigate: (view: any) => void;
}

const ReviewForm: React.FC<ReviewFormProps> = ({ paperId, onNavigate }) => {
  const [score, setScore] = useState<number>(5);
  const [comment, setComment] = useState<string>('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    console.log({
      paperId,
      score,
      comment,
    });

    alert('Đã gửi phản biện thành công!');
    onNavigate('reviewer-dashboard');
  };

  return (
    <div className="p-6 max-w-xl mx-auto">
      <h2 className="text-2xl font-bold mb-4">Phản biện bài báo</h2>

      <p className="mb-4">Mã bài báo: <strong>{paperId}</strong></p>

      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block mb-1">Điểm đánh giá (1–10)</label>
          <input
            type="number"
            min={1}
            max={10}
            value={score}
            onChange={(e) => setScore(Number(e.target.value))}
            className="border p-2 w-full"
            required
          />
        </div>

        <div>
          <label className="block mb-1">Nhận xét</label>
          <textarea
            value={comment}
            onChange={(e) => setComment(e.target.value)}
            className="border p-2 w-full"
            rows={4}
            required
          />
        </div>

        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded"
        >
          Gửi phản biện
        </button>
      </form>
    </div>
  );
};

export default ReviewForm;
