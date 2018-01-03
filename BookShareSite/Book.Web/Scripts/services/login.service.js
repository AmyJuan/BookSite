angular.module('bookApp').factory('LoginService', LoginService);

LoginService.$inject = ['$http', 'ServiceConfig'];

function LoginService($http, ServiceConfig) {
    return {
        load: function () {
            return $http.get('/Login/Load');
        },
        loadUsers: function () {
            return $http.get('/Login/LoadUsers');
        },
        saveUsers: function (datas) {
            return $http.post('/Login/SaveUsers', JSON.stringify({ datas: datas }));
        },
        getBooks: function () {
            $http.jsonp(ServiceConfig.book_search + '?callback=searchBookList&count=10&q=' + '三毛');
        },
        getBook: function (bookId) {
            $http.jsonp(ServiceConfig.book_search_id + bookId + '?callback=searchBook');
        },
        saveBooks: function (datas) {
            return $http.post('/Login/SaveBooks', JSON.stringify({ datas: datas }));
        },
        getUsers: function () {
            $http.jsonp(ServiceConfig.user_search + '?callback=searchUserList&count=100&q=1');
        },
        getBooksByUser: function (userId) {
            $http.jsonp(ServiceConfig.book_user_search + userId + '/collections?callback=searchCollections');
        },
        saveCollections: function (datas) {
            return $http.post('/Login/SaveCollections', JSON.stringify({ datas: datas }));
        },
        getReviews: function () {
            $http.jsonp(ServiceConfig.book_search_id + bookId + '/annotations?callback=searchBook');
        },
        test: function () {
            //$http.jsonp('https://api.douban.com/v2/book/user/1012727/collections?callback=test');
            //$http.jsonp('https://api.douban.com/v2/book/10795526?callback=test');
            //$http.jsonp('https://api.douban.com/v2/book/4177459/annotations?callback=test');
            //$http.jsonp('https://api.douban.com/v2/book/user/1012727/annotations?callback=test');
            //$http.jsonp('https://api.douban.com/v2/book/user_tags/12028067?callback=test');
            $http.jsonp('https://api.douban.com/v2/book/user/1012727/tags?callback=test');
            $http.jsonp('https://api.douban.com/v2/book/5254138/annotations');
        }
    }
}