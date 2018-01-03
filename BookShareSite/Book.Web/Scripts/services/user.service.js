app.factory('UserService', UserService);

UserService.$inject = ['$http', 'ServiceConfig'];

function UserService($http, ServiceConfig) {
    return {
        apiSave: function (data) {
            return $http.post('/User/ApiSave', JSON.stringify({ data: data }));
        },
        load: function (userId) {
            return $http.post('/User/Load', { id: userId });
        },
        getBooks: function (userId) {
            $http.jsonp(ServiceConfig.book_user_search + userId + '/collections?callback=searchCollections');
        },
    }
}