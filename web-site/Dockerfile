# Stage 1: Build the React app
FROM node:14 as build
WORKDIR /app

COPY package.json  ./

# Used to solve tempdorary package issue
#COPY package.json package-lock.json ./
#RUN npm cache clean -f
#RUN rm package-lock.json
RUN npm install

COPY . ./
RUN npm run build

# Stage 2: Serve the React app using Nginx
FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
