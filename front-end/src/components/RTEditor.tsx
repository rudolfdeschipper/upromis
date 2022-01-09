import React, {} from 'react';
import { ErrorMessage, Field, Form, Formik, useField } from 'formik';
import { Editor, IAllProps } from '@tinymce/tinymce-react';
import { Editor as TinyMCEEditor } from 'tinymce';

interface IRTEditorProps extends IAllProps {
  label?: string;
  name: string;
}


export const RTEditor = (props: IRTEditorProps) => {
    const {label, name, ...otherProps} = props;
    const [field, meta] = useField(name);
    const type = 'text';
    const handleEditorChange = (value: string, _editor: TinyMCEEditor) => {
      field.onChange({ target: { type, name, value } });
    };
  
    const handleBlur = (e: unknown, editor: TinyMCEEditor) => {
      field.onBlur({ target: { name } });
    };
  
    return (
      <>
        {label && <span className='label hide'>{label}</span>}
        <Editor 
          {...otherProps} 
          value={field.value} 
          onEditorChange={handleEditorChange} 
          onBlur={handleBlur}
          tinymceScriptSrc={process.env.PUBLIC_URL + '/tinymce/tinymce.min.js'}
          init={{
            height: 500,
            menubar: false,
            plugins: [
                'advlist autolink lists link image charmap print preview anchor',
                'searchreplace visualblocks code fullscreen',
                'insertdatetime media table paste code help wordcount'
            ],
            toolbar: 'undo redo | formatselect | ' +
                'bold italic backcolor | alignleft aligncenter ' +
                'alignright alignjustify | bullist numlist outdent indent | ' +
                'removeformat | help',
            content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:14px }'
        }} />
        {meta.touched && meta.error ? (
          <div className="error">{meta.error}</div>
        ) : null}
      </>
    );
  };
  