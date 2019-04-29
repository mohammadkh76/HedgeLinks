module.controller("tileController", ["$scope", "dataService", "$window", "Upload", function ($scope, dataService, $window, Upload) {
    $scope.data = {};
    $scope.changeDefaultLayout=function (layoutId){
        dataService.get('/api/TileApi/ChangeDefaultLayout/'+layoutId).then(function (res) {
            toastr.success("این نما به صورت پیش فرض انتخاب شد.");

        }).catch(function(err){
            console.log(err);
        })
        
        
    }
    $scope.changeTile = function (index) {
        $scope.tileInfo={
            Index:index,
            TileLayout:$scope.selectedLayout
        }
        dataService.post('/api/TileApi/GetTileInfo/' , $scope.tileInfo).then(function (res) {
        debugger
            $scope.data = res.data.rec;
            $scope.MenuPathId = $scope.data ? $scope.data.MenuPathId : 0;
            if (res.data.rec) {
                $scope.image = res.data.rec.ImagePath
            }
            else {
                $scope.image = null
            }

        })
        dataService.get('/api/TileApi/GetPages/').then(function (res) {
            $scope.pages = res.data.pages
            // $scope.data.MenuPathId=res.data.pages.Id;    
        })

    }
    dataService.get('/api/TileApi/GetLayout/').then(function (res) {
        $scope.layout = res.data;
    });
    $scope.changeLayout = function (id) {
        dataService.get('/api/TileApi/GetLayoutData/' + id).then(function (res) {
            $scope.hideLayoutPicture = false;

            if (!$scope.selectedLayout) {
                $scope.hideLayoutPicture = true;
            }
            const Count = res.data.tileLayout.Count;
            $scope.tileLayout = res.data.tileLayout;
            $scope.tileLayout.TourismHomeTiles = $scope.tileLayout.TourismHomeTiles || [];
            for (let i = 0; i < Count; ++i) {
                if (!$scope.tileLayout.TourismHomeTiles[i]) {
                    $scope.tileLayout.TourismHomeTiles[i] = {};
                }
            }
            console.log($scope.tileLayout);
            $scope.data = res.data;
        })
    }
    $scope.submitData = function (data) {
        if ($scope.selectedLayout == null || $scope.selectedTile == null) {
            toastr.error("ورودی های خود را کنترل کنید");
        }
        else {
            Upload.upload({
                url: '/api/TileApi/AddTile',
                data: {
                    Id: data.Id,
                    Index: $scope.selectedTile,
                    Title: data.Title,
                    Title_eng: data.Title_eng,
                    Title_ar: data.Title_ar,
                    Price: data.Price,
                    LayoutNumber: $scope.selectedLayout,
                    TileNumber: $scope.selectedTile,
                    File: $scope.File,
                    MenuPathId: $scope.MenuPathId,
                    Path: data.Path
                }
            }).then(function (re) {
                toastr.success(re.data.message);
                // $scope.selectedLayout = null;
                $scope.selectedTile = null;
                data.Title = null;
                data.Title_ar = null;
                data.Title_eng = null;
                data.Price = null;
                data.File = null;
                $scope.MenuPathId=null;
                data.Path=null;

            }).catch(function (err) {

            })


        }

    }
}]);