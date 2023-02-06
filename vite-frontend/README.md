# Vue 3 + Vite
## Recommended IDE Setup

- [VS Code](https://code.visualstudio.com/) + [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar) (and disable Vetur) + [TypeScript Vue Plugin (Volar)](https://marketplace.visualstudio.com/items?itemName=Vue.vscode-typescript-vue-plugin) + [Microsoft C# Extenstion](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

## Project setup

```ps
npm install
```

### Compiles and hot-reloads for development

```ps
npm run dev
```

### Assumptions
* You are working on the same directory as this README.md file. in the Terminal
* You have already run the backend project and it is running on port 5000
* You have already created the database and run the migrations in the C# project
* The database population file is under _.WebAPI > Database

### How To Change Default Port On Vite
* Open the file `vite.config.js`
* Change the port number on line 3
* Save the file
* Restart the project
* The project will now run on the new port number

