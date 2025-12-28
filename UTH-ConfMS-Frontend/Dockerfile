# Sử dụng Node image nhẹ
FROM node:18-alpine

# Thiết lập thư mục làm việc
WORKDIR /app

# Copy file định nghĩa thư viện
COPY package*.json ./

# Cài đặt thư viện
RUN npm install

# Copy toàn bộ code vào
COPY . .

# Mở cổng 3000 (Cổng mặc định chúng ta sẽ cấu hình cho Vite)
EXPOSE 3000

# Lệnh chạy server dev (thêm --host để Docker map được ra ngoài)
CMD ["npm", "run", "dev", "--", "--host"]