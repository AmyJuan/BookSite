//import * as angular from 'angular';
//let app = angular.module('login', []);

interface ILogin {
    name?: string;
}

LoginController.$inject = ["$scope"];
function LoginController($scope: ILogin) {
    $scope.name = "Xiao Ming";
}

export default function controller(app: ng.IModule) {
    debugger;
    app.controller('LoginController', LoginController);
}
