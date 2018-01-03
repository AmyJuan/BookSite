app.factory('ReviewService', ReviewService);

ReviewService.$inject = ['$http', 'ServiceConfig'];

function ReviewService($http, ServiceConfig) {
    return {
        apiSave: function (data) {
            return $http.post('/Review/ApiSave', JSON.stringify({ data: data }));
        },
        save: function (data) {
            return $http.post('/Review/Save', JSON.stringify({ data: data }));
        },
        load: function (bookId) {
            return $http.get('/Review/Load?id=', bookId);
        },
        loadBookAndReview: function (bookId, userId) {
            return $http.get('/Review/LoadBookAndReview?bookId=' + bookId + "&userId=" + userId);
        },
        getReviews: function (bookId) {
            $http.jsonp(ServiceConfig.book_search_id + bookId + '/annotations?callback=searchReview');
        },
    }
}