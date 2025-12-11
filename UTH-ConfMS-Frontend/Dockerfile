# Build React App
FROM node:18-alpine as build
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Chạy bằng Nginx
FROM nginx:alpine
# Copy file build vào thư mục của Nginx
COPY --from=build /app/dist /usr/share/nginx/html
# (Lưu ý: Nếu dùng Create-React-App cũ thì đổi /dist thành /build)

RUN echo 'server { \
    listen 80; \
    location / { \
        root /usr/share/nginx/html; \
        index index.html index.htm; \
        try_files $uri $uri/ /index.html; \
    } \
}' > /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]