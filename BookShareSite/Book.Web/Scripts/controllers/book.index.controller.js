app.controller('BookIndexController', BookIndexController)
BookIndexController.$inject = ['$scope', 'BookService'];
function BookIndexController($scope, BookService) {
    $scope.pageIndex = 1;
    $scope.pageTotal = 10;
    $scope.isActive = false;
    init();
    $scope.next = function () {
        if ($scope.pageIndex < $scope.pageTotal) {
            $scope.pageIndex++;
            console.log("$scope.pageIndex,{0}", $scope.pageIndex);
            init();
        }
    }

    $scope.previous = function () {
        if ($scope.pageIndex > 1) {
            $scope.pageIndex--;
            console.log("$scope.pageIndex,{0}", $scope.pageIndex);
            init();
        }
    }

    $scope.loadBook = function (book) {
        window.location.href  = "/Book/ViewBook?id="+book.ID;
    }

    function init() {
        var pageSize = 10;
        BookService.load($scope.pageIndex, pageSize)
       .success(function (result) {
           if (!!result && !!result.Books && result.Books.length > 0) {
               $scope.books = result.Books;
               $scope.pageTotal = result.PageTotal;
           }
       })
       .error(function (e) {
           console.log("login load error {0}", e)
       })
    }
}