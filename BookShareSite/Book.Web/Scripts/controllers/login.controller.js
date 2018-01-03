app.controller('loginController', LoginController)
LoginController.$inject = ['$scope', 'LoginService'];
function LoginController($scope, LoginService) {
    LoginService.load()
        .success(function (result) {
            //$scope.user = {
            //    Name: result
            //};
        })
        .error(function (e) {
            console.log("login load error {0}", e)
        })
    $scope.user = {};

    $scope.transBooks = function () {
        LoginService.loadUsers()
           .success(function (userIds) {
               console.log("load users: {0}", userIds)
               angular.forEach(userIds, function (userId) {
                   setTimeout(SaveBooksByUser(userId), 100000)
               })
           })
           .error(function (e) {
               console.log("load users error {0}", e)
           })
    }

    $scope.test = function () {
        LoginService.test()
        window.test = function (data) {
            console.log("test is:  {0}", data)
        }
    }

    $scope.getBooks = function () {
        LoginService.getBooks()
        window.searchBookList = function (data) {
            console.log("books is:  {0}", data)
            var datas = [];
            angular.forEach(data.books, function (book) {
                var b = {
                    ID: book.id,
                    Title: book.title,
                    OriginTitle: book.origin_title,
                    Summary: book.summary,
                    Image: book.image,
                    Authors: book.author,
                    Translators: book.translator
                }
                datas.push(b);
            })
            SaveBooks(datas);
        }
    }

    $scope.getUsers = function () {
        LoginService.getUsers()
        window.searchUserList = function (data) {
            console.log("User is:  {0}", data)
            var datas = [];
            angular.forEach(data.users, function (user) {
                var u = {
                    ID: user.id,
                    Uid: user.uid,
                    Name: user.name,
                    Password: '',
                    Alt: user.alt,
                    Image: user.avatar,
                    Create: !user.created ? 0 : Date.parse(user.created)
                }
                datas.push(u);
            })
            SaveUsers(datas);
        }
    }

    function SaveBooksByUser(userId) {
        LoginService.getBooksByUser(userId);
        window.searchCollections = function (data) {
            console.log("get Collections is:  {0}, {1}", userId, data)
            var datas = [];
            var collections = [];
            var index = 0;
            angular.forEach(data.collections, function (col) {
                index++;
                //var book = col.book;
                //var b = {
                //    ID: book.id,
                //    Title: book.title,
                //    OriginTitle: book.origin_title,
                //    Summary: book.summary,
                //    Image: book.image,
                //    Authors: book.author,
                //    Translators: book.translator
                //}
                //datas.push(book);
                var c = {
                    ID: col.id,
                    BookId: col.book_id,
                    UserId: col.user_id,
                    Status: col.status,
                    Updated: !col.updated ? 0 : Date.parse(col.updated)
                }
                collections.push(c);
                if (index == data.collections.length) {
                    //SaveBooks(datas);
                    SaveCollections(collections);
                }
            })
        }
    }

    //function GetBook(bookId, callback) {
    //    LoginService.getBook(bookId);
    //    window.searchBook = function (book) {
    //        console.log("get Book is:  {0}", book)
    //        var b = {
    //            ID: book.id,
    //            Title: book.title,
    //            OriginTitle: book.origin_title,
    //            Summary: book.summary,
    //            Image: book.image,
    //            Authors: book.author,
    //            Translators: book.translator
    //        }
    //        callback(b);
    //    }
    //}

    function SaveCollections(datas) {
        LoginService.saveCollections(datas)
           .success(function (result) {
               $scope.user.Name = result;
               console.log("save collections {0}", result)
           })
           .error(function (e) {
               console.log("save collections error {0}", e)
           })
    }

    function GetBooksByUser(userId) {
        LoginService.GetBooksByUser(userId);
        window.searchBooks = function (data) {
            console.log("get Books by user is:  {0}, {1}", userId, data)
        }
    }

    function SaveBooks(datas) {
        LoginService.saveBooks(datas)
           .success(function (result) {
               $scope.user.Name = result;
               console.log("save books {0}", result)
           })
           .error(function (e) {
               console.log("save books error {0}", e)
           })
    }

    function SaveUsers(datas) {
        LoginService.saveUsers(datas)
            .success(function (result) {
                $scope.user.Name = result;
                console.log("save user {0}", result)
            })
            .error(function (e) {
                console.log("save user error {0}", e)
            })
    }
}