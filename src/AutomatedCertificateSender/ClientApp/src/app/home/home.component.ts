import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  private sheetId: string;

  ngOnInit(): void {
    this.getConfiguration();
    this.getListOfParticipants();
  }

  async getConfiguration(): Promise<void> {

    while (true) {

      try {
        console.log("Fetching configuration");
        const response = await fetch("/api/config");
        const jsonResponse = await response.text();

        console.log(jsonResponse);

        break;

      } catch (e) {
        console.log(e)
      }

      await this.sleep(5000);
    }
  }

  getListOfParticipants(): void {

    setInterval(async () => {

      if (!this.sheetId) return;

      const response = await fetch("/api/excel/values?" + new URLSearchParams({
        sheetId: this.sheetId,
        range: "A1:I101"
      }));

    }, 5000)
  }


  sleep(ms): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

}
