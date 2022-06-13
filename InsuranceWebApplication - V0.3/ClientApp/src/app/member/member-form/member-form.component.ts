import { Component, OnInit } from '@angular/core';
import { MemberService } from '../../shared/member.service';
import { NgForm } from '@angular/forms';
import { Member } from '../../shared/member.model';
@Component({
  selector: 'app-member-form',
  templateUrl: './member-form.component.html',
  styles: [
  ]
})
export class MemberFormComponent implements OnInit {
  constructor(public service: MemberService) {
  }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id == 0) //we will use the id as identifier for updating or insertion
      this.insertRecord(form);
    else
      this.updateRecord(form);
  }

  calculatePremium() {
    this.service.calculatePremium(this.service.formData)
      .subscribe(
        res => {
          this.service.formData.premium = res.toString();
          this.service.refreshList();
        },
        err => { console.log(err) }
    )

    this.service.formData.premium = "5";
  }

  insertRecord(form: NgForm) {
    this.service.postMember().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
      },
      err => {
        console.log(err);
      }
    );
  }
  updateRecord(form: NgForm) {
    this.service.putMember().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
      },
      err => {
        console.log(err);
      }
    );
  }
  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new Member();
  }
}
