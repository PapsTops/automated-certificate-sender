import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-configure-component',
  templateUrl: './configure.component.html'
})
export class ConfigureComponent implements OnInit {
  constructor(private http: HttpClient) { }

  currentSourceFile: GDriveFile;
  availableExcelFiles: GDriveFile[];

  ngOnInit(): void {
    this.getListOfExcelFile();
    this.getCurrentConfig();
  }
  async getCurrentConfig() {
    while (true) {
      if (this.availableExcelFiles) {
        this.http.get<ConfigResponse>("/api/config").subscribe(result => {
          
          var gDriveFile = this.availableExcelFiles.filter(x => x.id == result.source.sheetId)

          if (gDriveFile) {
            this.currentSourceFile = gDriveFile[0]
          }
          
        }, error => console.error(error));
        break;
      }
      await this.sleep(1000);
    }

  }

  getListOfExcelFile() {
    this.http.get<GDriveQueryResult>("/api/excel/list").subscribe(result => {
      console.log(result)
      this.availableExcelFiles = result.files;
      console.log(this.availableExcelFiles)
    }, error => console.error(error));
  }

  sleep(ms): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  onSubmit(excelFileForm): void {
    const sheetId: string = excelFileForm.value.selectedFile;

    if (sheetId) {
      this.http.post("/api/config", {
        sheetId: sheetId
      }).subscribe(result => {
        alert("Success");
      }, error => console.log(error))
    }

  }
}

export class GDriveFile {
  public mimeType: string;
  public kind: string;
  public name: string;
  public id: string;
}

export class GDriveQueryResult {
  public files: Array<GDriveFile>;
}

export class ConfigResponse {
  source: Source
}

export class Source {
  sheetId: string;
}
