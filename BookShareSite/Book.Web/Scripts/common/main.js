var app = angular.module('bookApp', []);

/*服务的URL配置*/
app.constant('ServiceConfig', {
    book_search: 'https://api.douban.com/v2/book/search',
    book_search_id: 'https://api.douban.com/v2/book/',
    user_search: 'https://api.douban.com/v2/user',
    book_user_search: 'https://api.douban.com/v2/book/user/'
});

//app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

//}])