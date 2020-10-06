# Multiple File Upload and Image Resizer(SixLabors)
.Net Core Web Api | Multiple File Upload and Image Resizer(SixLabors)

```bash
<form enctype="multipart/form-data">
    <input id="file" name="file" type="file" multiple />
        <button type="button" onclick="UploadFile()">Upload</button>
    <img id="uploadImage" name="uploadImage" />
</form>
```

```bash
<script>
    function UploadFile() {
        var baseUrl = "http://localhost:5000";
        var serviceUrl = "/media/multi-upload";

        var fd = new FormData();
        var files = document.getElementById('file').files.length;
        for (var x = 0; x < files; x++) {
            fd.append("file", document.getElementById('file').files[x]);
        }

        $.ajax({
            type: "POST",
            url: baseUrl + serviceUrl,
            data: fd,
            dataType:"json",
            processData: false,
            contentType: false,
            success: function (data) {
                var r = data.value;
                console.log(r);
                },
                error: function (data) {
                    var r = data.responseJSON.error;
                        alert(r);
                    }
                    });
    }
</script> 
```