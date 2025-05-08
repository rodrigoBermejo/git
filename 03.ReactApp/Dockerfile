FROM node:lts-alpine

# Create app directory
WORKDIR /app

# Install app dependencies
COPY package.json ./
RUN npm install

# Copy app source code
COPY . .

# Expose the port the app runs on
EXPOSE 3000

# Build the app
RUN npm run build

# Run the app
CMD ["npm", "start"]