# .Net Core Web Api | Multiple File Upload and Image Resizer(SixLabors)
.Net Core Web Api | Multiple File Upload and Image Resizer(SixLabors)

```bash
<form enctype="multipart/form-data">
    <input id="file" name="file" type="file" multiple onchange="UploadFile()" />
    <div id="uploadedImagesDiv" class="row sortable-list"></div>
</form>
```

```bash
<script>
    var uploadedImages = [];
    var baseUrl = "http://localhost:5001";
    var serviceUrl = "/media/multipleupload";

    function UploadFile() {

        var fd = new FormData();
        var files = document.getElementById('file').files.length;
        for (var x = 0; x < files; x++) {
            fd.append("file", document.getElementById('file').files[x]);
        }

        $.ajax({
            type: "POST",
            url: baseUrl + serviceUrl,
            data: fd,
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (data) {
                var r = data.value;

                r.map(function (item) {
                    uploadedImages.push(item);
                });

                ShowImages();
            },
            error: function (data) {
                var r = data.responseJSON.error;
                alert(r);
            }
        });
    }

    function ShowImages() {
        var html = "";
        uploadedImages.map(function (item, index) {
            html += '<div class="col-md-3" style="cursor:move">';
            html += '<img src="' + baseUrl + '/upload/small/' + item.fileName + '" alt="" />';
            html += '<button onclick="DeleteImage(' + index + ')" type="button" class="btn btn-danger btn-xs" style="position:absolute;margin-left:-35px;"><i class="fas fa-times"></i></button>';
            html += '</div>'
        });

        $('#uploadedImagesDiv').html(html);

        Sortable();
    }

    function DeleteImage(item) {
        uploadedImages.splice(item, 1);
        ShowImages();
    }

    function Sortable() {
        var manipulate, oldIndex;
        $(".sortable-list").sortable({
            axis: "x",
            containment: ".sortable-list",
            revert: true,

            start: function (event, ui) {
                var updt = ui.item.index();
                manipulate = updt;
                oldIndex = uploadedImages[manipulate];
            },

            update: function (event, ui) {
                var newIndex = ui.item.index();
                uploadedImages.splice(manipulate, 1);
                uploadedImages.splice(newIndex, 0, oldIndex);
            }
        });

        $(".sortable-list").disableSelection();
    }
</script>  
```
