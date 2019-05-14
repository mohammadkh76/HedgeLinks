adminModule.controller('InsertMenupathController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
    

    $scope.tinymceOptions = {
        plugins: 'fullpage powerpaste searchreplace autolink directionality visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor wordcount imagetools media link contextmenu colorpicker textpattern',
        toolbar1: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat imageupload',
        setup: function (editor) {
            var inp = $('<input id="tinymce-uploader" type="file" name="pic" accept="image/*" style="display:none">');
            $(editor.getElement()).parent().append(inp);

            inp.on("change", function () {
                var input = inp.get(0);
                var file = input.files[0];
                var fr = new FileReader();
                fr.onload = function () {
                    var img = new Image();
                    img.src = fr.result;
                    editor.insertContent('<img src="' + img.src + '"/>');
                    inp.val('');
                }
                fr.readAsDataURL(file);
            });

            editor.addButton('imageupload', {
                text: "IMAGE",
                icon: false,
                onclick: function (e) {
                    inp.trigger('click');
                }
            });
        },
    };
  $scope.confirmInsert = function () {
        $scope.toSendData = {
            Name: $scope.insert.Name,
            Description: $scope.Description,
            PageName: $scope.insert.PageName

        }
        dataService.post($scope.url, $scope.toSendData).then(function (res) {
            if (res.data.Status == "success") {
               
                //window.location.href = $scope.href;
                $scope.cancel();



            }
        })
    }
    
    $uibModalInstance.result.then(function () {
        $scope.tableLoading = true;
        $scope.getAll({
            successFunc() {
                $scope.tableLoading = false;
            },
            messages() {
                toaster.pop('success', 'Success', 'Your Record inserted successfully');
                 
            }
        });

    });
    $scope.cancel = function () {
        $uibModalInstance.close();

    }
   
}]);