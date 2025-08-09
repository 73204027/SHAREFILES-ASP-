const form = document.getElementById('uploadForm');
    form.addEventListener('submit', async (e) => {
      e.preventDefault();
      const formData = new FormData(form);

      const response = await fetch("upload", {
        method: 'POST',
        body: formData,
      });

      const text = await response.text();
      alert(text);
    });