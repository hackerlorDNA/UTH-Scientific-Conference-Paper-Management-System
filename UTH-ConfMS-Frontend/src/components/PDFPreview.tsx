import React from 'react';

interface PDFPreviewProps {
  fileName: string;
  fileUrl?: string;
  fileSize?: number;
  uploadedAt?: string;
}

export const PDFPreview: React.FC<PDFPreviewProps> = ({ fileName, fileUrl, fileSize, uploadedAt }) => {
  const formatSize = (bytes?: number) => {
    if (!bytes) return 'Unknown size';
    if (bytes < 1024) return bytes + ' B';
    if (bytes < 1024 * 1024) return (bytes / 1024).toFixed(1) + ' KB';
    return (bytes / (1024 * 1024)).toFixed(1) + ' MB';
  };

  const formatDate = (dateStr?: string) => {
      if (!dateStr) return '';
      return new Date(dateStr).toLocaleDateString();
  };

  return (
    <div className="w-full h-full bg-gray-100 dark:bg-gray-900 rounded-lg border border-border-light dark:border-border-dark flex flex-col items-center justify-center p-8 text-center relative overflow-hidden group">
      <div className="absolute inset-0 bg-white dark:bg-gray-800 opacity-0 group-hover:opacity-10 transition-opacity"></div>
      
      <span className="material-symbols-outlined text-[64px] text-red-500 mb-4 drop-shadow-sm">picture_as_pdf</span>
      <h3 className="font-bold text-text-main-light dark:text-text-main-dark mb-1">{fileName}</h3>
      <p className="text-xs text-text-sec-light dark:text-text-sec-dark mb-6">
          {formatSize(fileSize)} {uploadedAt && `• Uploaded ${formatDate(uploadedAt)}`}
      </p>
      
      <div className="flex gap-3">
        {fileUrl && (
            <a href={fileUrl} target="_blank" rel="noreferrer" className="flex items-center gap-2 px-4 py-2 bg-white dark:bg-card-dark border border-border-light dark:border-border-dark rounded-lg text-sm font-medium hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors shadow-sm">
                <span className="material-symbols-outlined text-[18px]">visibility</span> Preview
            </a>
        )}
        <button className="flex items-center gap-2 px-4 py-2 bg-primary text-white rounded-lg text-sm font-medium hover:bg-primary-hover transition-colors shadow-sm">
            <span className="material-symbols-outlined text-[18px]">download</span> Download
        </button>
      </div>
    </div>
  );
};
