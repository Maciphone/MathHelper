
FROM node:16-alpine as build


WORKDIR /app

# Függőségek másolása
COPY package.json package-lock.json ./
RUN npm install

# Alkalmazás és tanúsítványok másolása
COPY . .
COPY localhost-cert.pem /app/localhost-cert.pem
COPY localhost-key.pem /app/localhost-key.pem

# Build
RUN npm run build

# Alap image futtatáshoz
FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html

# HTTPS portok
EXPOSE 443
CMD ["nginx", "-g", "daemon off;"]
