import { Component, ElementRef, Input, OnInit, Self, ViewChild } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({standalone:false,
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('input', { static: true }) input!: ElementRef;
  @Input() type = 'text';
  @Input() label = 'string';

  onChange: any = () => {};
  onTouched: any = () => {};

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDir.control;
    if (control) {
      const validators = control.validator ? [control.validator] : [];
      const asyncValidators = control.asyncValidator ? [control.asyncValidator] : [];

      control.setValidators(validators);
      control.setAsyncValidators(asyncValidators);
      control.updateValueAndValidity();
    }
  }

  // ** هنا تضيف الدالتين اللي طلبتهم **
onInput(event: Event) {
  const value = (event.target as HTMLInputElement).value;
  this.onChange(value);
}

onBlur() {
  this.onTouched();
}


  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  
}
