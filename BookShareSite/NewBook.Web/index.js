"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
LoginController.$inject = ["$scope"];
function LoginController($scope) {
    $scope.name = "Xiao Ming";
}
function controller(app) {
    app.controller('LoginController', LoginController);
}
exports.default = controller;
//# sourceMappingURL=index.js.map