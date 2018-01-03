app.controller('BookViewController', BookViewController)
BookViewController.$inject = ['$scope', 'BookService', 'UtilService'];
function BookViewController($scope, BookService, UtilService) {
    var id = UtilService.getRequest()['id'];
    init();
    $scope.back = function () {
        window.location.href = "/Book/Index";
    }

    $scope.addReview = function () {
        window.location.href = "/Review/Add?id=" + id;
    }

    $scope.getReviews = function () {
        window.location.href = "/Review/Index?id=" + id;
    }

    $scope.goUser = function (userId) {
        window.location.href = "/User/Index?id=" + userId;
    }

    function init() {
        BookService.loadBookAndReviews(id)
       .success(function (result) {
           if (!!result.Book) {
               $scope.book = result.Book;
               var reviews = [];
               angular.forEach(result.Reviews, function (item) {
                   if (reviews.length > 0 && reviews.filter(c=>c.UserId == item.UserId).length > 0) {
                       reviews.filter(c=>c.UserId == item.UserId)[0].Content += " " + item.Content;
                   }
                   else {
                       reviews.push(item)
                   }
               })
               $scope.reviews = reviews.slice(0, 5);
           }
       })
       .error(function (e) {
           console.log("login load error {0}", e)
       })
    }
}