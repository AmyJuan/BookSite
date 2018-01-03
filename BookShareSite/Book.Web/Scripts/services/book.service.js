app.factory('BookService', BookService);

BookService.$inject = ['$http', 'ServiceConfig'];

function BookService($http, ServiceConfig) {
    return {
        save: function (data) {
            return $http.post('/Book/Save', JSON.stringify({ data: data }));
        },
        load: function (pageIndex, pageSize) {
            return $http.get('/Book/Load?pageIndex=' + pageIndex + "&pageSize=" + pageSize);
        },
        loadBookAndReviews: function (id) {
            return $http.get('/Book/LoadBookAndReviews?id=' + id);
        },
        loadBook: function (id) {
            return $http.get('/Book/LoadBook?id=' + id);
        },
    }
}