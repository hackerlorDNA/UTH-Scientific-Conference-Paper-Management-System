import React, { useState } from 'react';

interface ConferenceManagementProps {
  onNavigate: (view: any) => void;
}

const ConferenceManagement: React.FC<ConferenceManagementProps> = () => {
  const [conferenceName, setConferenceName] = useState('');
  const [deadline, setDeadline] = useState('');

  const handleSave = () => {
    console.log({
      conferenceName,
      deadline,
    });

    alert('Đã cập nhật hội nghị!');
  };

  return (
    <div className="p-6 max-w-xl mx-auto">
      <h2 className="text-2xl font-bold mb-4">Quản lý hội nghị</h2>

      <div className="space-y-4">
        <div>
          <label className="block mb-1">Tên hội nghị</label>
          <input
            type="text"
            value={conferenceName}
            onChange={(e) => setConferenceName(e.target.value)}
            className="border p-2 w-full"
          />
        </div>

        <div>
          <label className="block mb-1">Hạn nộp bài</label>
          <input
            type="date"
            value={deadline}
            onChange={(e) => setDeadline(e.target.value)}
            className="border p-2 w-full"
          />
        </div>

        <button
          onClick={handleSave}
          className="bg-purple-600 text-white px-4 py-2 rounded"
        >
          Lưu cấu hình
        </button>
      </div>
    </div>
  );
};

export default ConferenceManagement;
