app.controller('UserIndexController', UserIndexController)
UserIndexController.$inject = ['$scope', 'UserService', 'UtilService'];
function UserIndexController($scope, UserService, UtilService) {
    var id = UtilService.getRequest()['id'];
    $scope.addResult = "";
    init()

    $scope.getBooks = function () {
        GetBooksInAPI(id);
    }

    function init() {
        UserService.load(id)
            .success(function (result) {
                $scope.user = result;
                $scope.user.CreateStr = new Date(result.Create);
            })
           .error(function (e) {
               console.log("Save Books And Collections error {0}", e)
           })
    }

    function GetBooksInAPI(userId) {
        UserService.getBooks(userId)
        window.searchCollections = function (data) {
            console.log("get Collections is:  {0}, {1}", userId, data)
            var books = [];
            var collections = [];
            angular.forEach(data.collections, function (col) {
                var book = col.book;
                var b = {
                    ID: book.id,
                    Title: book.title,
                    OriginTitle: book.origin_title,
                    Summary: book.summary,
                    Image: book.image,
                    Authors: book.author,
                    Translators: book.translator
                }
                books.push(book);
                var c = {
                    ID: col.id,
                    BookId: col.book_id,
                    UserId: col.user_id,
                    Status: col.status,
                    Updated: !col.updated ? 0 : Date.parse(col.updated)
                }
                collections.push(c);
            })
            console.log("users is:  {0}, reviews is {1}", books, collections);
            SaveBooksAndCollections(books, collections);
        }
    }

    function SaveBooksAndCollections(books, collections) {
        UserService.apiSave({ Books: books, Collections: collections })
            .success(function (result) {
                $scope.addResult = "Successful"
                console.log("successful")
                //$scope.user.Name = result;
                //console.log("save collections {0}", result)
            })
           .error(function (e) {
               $scope.addResult = "Error"
               console.log("Save Books And Collections error {0}", e)
           })
    }

}