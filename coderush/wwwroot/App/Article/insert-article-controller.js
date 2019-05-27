adminModule.controller('insertArticleController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
    $scope.isCanceled = false;
    $scope.menubarLoading = true;
    dataService.get('/api/Menupath/GetAllMenupath/').then(function (res) {
        if (res.data.Status == "Success") {
            
            $scope.page= res.data.Data;
            $scope.$apply();


        }
        $scope.menubarLoading = false;

    })
    dataService.get('/api/ArticleTopic/GetAllArticleTopic/').then(function (res) {
        if (res.data.Status == "Success") {

            $scope.articleTopic = res.data.Data;
            $scope.$apply();

        }
        $scope.menubarLoading = false;

    })

    $scope.confirmInsert = function () {
        $scope.toSendData = {
            AuthorName: $scope.AuthorName,
            Title: $scope.Title,
            Description: $scope.Description,
            Date: $scope.Date,
            Keyword: $scope.Keyword,
            ArticleTopicId: $scope.SelectedArticleTopic,
            MenupathId: $scope.SelectedPage,
            ExternalLink: $scope.ExternalLink,
            isShow:$scope.isShow
        }
        dataService.post($scope.url, $scope.toSendData).then(function (res) {
            if (res.data.Status == "success") {

                //window.location.href = $scope.href;
                $uibModalInstance.close();



            }
        })
    }

    $uibModalInstance.result.then(function () {
        if (!$scope.isCanceled) {

            $scope.tableLoading = true;
            $scope.getAll({
                successFunc() {
                    $scope.tableLoading = false;
                },
                messages() {
                    toaster.pop('success', 'Success', 'Your Record inserted successfully');

                }
            });
        }

    });

    $scope.cancel = function () {
        $scope.isCanceled = true;
        $uibModalInstance.close();

    }

}]);