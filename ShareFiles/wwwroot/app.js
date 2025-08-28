const form = document.getElementById('uploadForm');
const progressBar = document.getElementById('progressBar');
const api_url = "/api/Main/upload";

function uploadFiles() {
  // check input validity
  const files = document.getElementById("fileInput").files;

  if (files.length == 0 || files == null) {
    alert("Hey, select at least one file!");
    return;
  }


  // execute upload
  const formData = new FormData();

  // manually load files into formData
  for (let i=0; i<files.length; i++){
    formData.append("files", files[i]);
  }

  // set the XML Http Request (xhr)
  const xhr = new XMLHttpRequest();

  xhr.open("POST", api_url, true);
  xhr.responseType = "json";
  xhr.upload.onprogress = (e) => {
    if (e.lengthComputable) {
      const percent = Math.round((e.loaded/e.total) * 100);
      progressBar.style.width = percent + "%";
      progressBar.textContent = percent + "%";
    }
  }

  // on success
  xhr.onload = () => {
    if (xhr.status === 200){

      // get response
      const serv_response = xhr.response;
      alert(
        `Uploaded Files: ${serv_response.Uploaded_Files}\n` +
        `Skipped Files: ${serv_response.Skipped_Files}\n` +
        `Success Rate: ${serv_response.Success_Rate}`
      );

    }
  }

  // send the XML Request
  xhr.send(formData);
}

// submit button event listener
form.addEventListener('submit', async (e) => {
  e.preventDefault();
  uploadFiles();
});

