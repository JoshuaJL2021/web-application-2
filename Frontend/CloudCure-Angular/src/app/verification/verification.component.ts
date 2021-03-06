import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, FormControlName, ReactiveFormsModule } from '@angular/forms';
import { CovidService } from 'src/app/services/covid.service';
import { Router } from '@angular/router';
import { CovidVerify } from '../AngularModels/CovidVerify';
import { PatientService } from '../services/patient.service';

@Component({
  selector: 'app-verification',
  templateUrl: './verification.component.html',
  styleUrls: ['./verification.component.css']
})

export class VerificationComponent implements OnInit {

  verifyGroup: FormGroup = new FormGroup({
    'question1': new FormControl(),
    'question2': new FormControl(),
    'question3': new FormControl(),
    'question4': new FormControl(),
    'question5': new FormControl()
  })

  constructor(private fb: FormBuilder, private router: Router, private covidService: CovidService, private patientAPI:PatientService) {

  }

  ngOnInit(): void {
    console.log(this.covidService.patient.userProfile.id)
  }
  
    submitForm(verifyGroup: FormGroup) {
      console.log(this.patientAPI.currentPatientId);
      if (verifyGroup.valid) {
        let Info: CovidVerify = {
          userId: this.covidService.patient.userProfile.id,
          question1: verifyGroup.get("question1")?.value,
          question2: verifyGroup.get("question2")?.value,
          question3: verifyGroup.get("question3")?.value,
          question4: verifyGroup.get("question4")?.value,
          question5: verifyGroup.get("question5")?.value
        }
        console.log(Info);
        console.log(this.patientAPI.currentPatientId);
        this.covidService.Add(Info).subscribe(
          (response) => {
            console.log(response);
            this.router.navigateByUrl("");
          }
        )
        console.log(this.verifyGroup.value);
      }
  }
}

