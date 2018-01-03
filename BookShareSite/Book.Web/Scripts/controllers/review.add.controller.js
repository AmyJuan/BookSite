app.controller('ReviewAddController', ReviewAddController)
ReviewAddController.$inject = ['$scope', 'BookService', 'ReviewService', 'UtilService'];

function ReviewAddController($scope, BookService, ReviewService, UtilService) {
    var id = UtilService.getRequest()['id'];
    $scope.content = "";
    $scope.saveResult = "";
    init();
    $scope.Save = function () {
        var review = {
            BookId: id,
            UserId: '3616295',
            Updated: new Date().getTime(),
            Content: $scope.content
        }
        ReviewService.save(review)
            .success(function (result) {
                $scope.saveResult = "Successful"
            })
           .error(function (e) {
               $scope.saveResult = "Error"
               console.log("save collections error {0}", e)
           })
    }

    function init() {
        ReviewService.loadBookAndReview(id, "3616295")
           .success(function (result) {
               $scope.book = result.Book;
               $scope.content = !result.Review ? "" : result.Review.Content;
           })
          .error(function (e) {
              console.log("save collections error {0}", e)
          })
    }

}