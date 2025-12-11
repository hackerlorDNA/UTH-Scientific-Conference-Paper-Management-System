export default function Home() {
  return (
    <div className="bg-[#DFF4E5] min-h-screen">
      {/* Header */}
      <header className="bg-white shadow-sm py-4 px-8 flex justify-between">
        <h1 className="text-2xl font-bold text-[#4FAD62]">UTH-ConfMS</h1>
        <nav className="flex gap-6 text-gray-700">
          <a href="#">Home</a>
          <a href="#">CFP</a>
          <a href="#">Submit</a>
          <a href="#">Committee</a>
        </nav>
        <button className="bg-[#4FAD62] text-white px-4 py-2 rounded-lg">
          Đăng nhập
        </button>
      </header>

      {/* Hero */}
      <section className="text-center py-20">
        <h2 className="text-4xl font-bold text-[#4FAD62]">
          Hệ thống Quản lý Hội nghị Khoa học UTH
        </h2>
        <p className="text-gray-700 mt-4 max-w-2xl mx-auto">
          Tích hợp toàn bộ quy trình hội nghị từ nộp bài đến xuất chương trình.
        </p>

        <div className="mt-8 flex justify-center gap-4">
          <button className="bg-[#4FAD62] text-white px-6 py-3 rounded-lg">
            Xem CFP
          </button>
          <button className="bg-white text-[#4FAD62] border border-[#4FAD62] px-6 py-3 rounded-lg">
            Nộp bài ngay
          </button>
        </div>
      </section>

      {/* Workflow */}
      <section className="bg-white py-16 px-8 rounded-t-3xl">
        <h3 className="text-3xl font-semibold text-center text-[#4FAD62]">
          Quy trình hội nghị
        </h3>

        <div className="grid grid-cols-3 md:grid-cols-6 gap-4 mt-10">
          {["CFP","Nộp bài","Phân công","Đánh giá","Quyết định","Camera-ready"]
            .map((step, i) => (
              <div key={i} className="p-4 bg-white border border-[#DFF4E5] rounded-xl text-center">
                <p className="font-medium text-gray-700">{step}</p>
              </div>
          ))}
        </div>
      </section>
    </div>
  );
}
