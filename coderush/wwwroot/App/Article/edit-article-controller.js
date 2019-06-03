adminModule.controller('editArticleController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    console.log("in edit modal conroller")
    $scope.editDataLoading = true;
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
    dataService.get('/api/Menupath/GetAllMenupath/').then(function (res) {
        if (res.data.Status == "Success") {
            $scope.page = res.data.Data;
            $scope.$apply();
        }
        $scope.menubarLoading = false;

    })
    dataService.get('/api/ArticleTopic/GetAllArticleTopic/').then(function (res) {
        if (res.data.Status == "Success") {

            $scope.articleTopic = res.data.Data;
            $scope.$apply();


        }
    })
    $scope.clearPath = function () {
        $scope.ExternalLink = "";

    }
    $scope.editDataLoading = true;

    dataService.get($scope.url).then(function (res) {
        if (res.data.Status == "success") {
            $scope.ExternalLink = res.data.Data.ExternalLink;
            $scope.Title = res.data.Data.Title;
            $scope.AuthorName = res.data.Data.AuthorName;
            $scope.Description = res.data.Data.Description;
            $scope.Date = res.data.Data.Date;
            $scope.Keyword = res.data.Data.Keyword;
            $scope.selectedId = res.data.Data.Id;
            $scope.SelectedArticleTopic = res.data.Data.ArticleTopicId.toString()
            $scope.isShow = res.data.Data.isShow;
            $scope.SelectedPage = (res.data.Data.MenuPathId) ? res.data.Data.MenuPathId.toString():'0';
            $scope.editDataLoading = false;

        }


    })
    $scope.confirmEdit = function () {
        
        $scope.editData = {
            Id:$scope.selectedId,
            AuthorName: $scope.AuthorName,
            Title: $scope.Title,
            Description: $scope.Description,
            Date: $scope.Date,
            Keyword: $scope.Keyword,
            ArticleTopicId: $scope.SelectedArticleTopic,
            MenuPathId: $scope.SelectedPage,
            ExternalLink: $scope.ExternalLink,
            isShow: $scope.isShow
        }
        debugger;
        dataService.post('/api/Article/Edit/', $scope.editData).then(function (res) {
            if (res.data.Status == 'success') {
                $uibModalInstance.close();
            }

        })
        $uibModalInstance.result.then(function () {
            if (!$scope.isCanceled) {
                $scope.tableLoading = true;
                $scope.getAll({
                    successFunc() {
                        $scope.tableLoading = false;
                    },
                    messages() {
                        toaster.pop('success', 'Success', 'Your Record edited successfully');

                    }
                });

            }
           
        });
        $scope.cancel = function () {
            $scope.isCanceled = true;
            $uibModalInstance.close();
        }

    }
}])